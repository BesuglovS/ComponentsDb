using ComponentsDb.Context;
using ComponentsDb.DomainClasses;
using ComponentsDb.InputDialog;
using ComponentsDb.OpenXml;
using ComponentsDb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ComponentsDb
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            MainView.NodeMouseClick += (sender, args) => MainView.SelectedNode = args.Node;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DatabaseSeeder.SeedData1();

            RefreshView();            
        }

        private void RefreshView()
        {
            MainView.Nodes.Clear();

            var repo = new ComponentsRepo();

            var topLevelComponents = repo.Components
                .FindAllIncluded(c => c.IsTopLevel)
                .OrderBy(c => c.Name);

            foreach (var topLevelComponent in topLevelComponents)
            {
                var node = MainView.Nodes.Add(topLevelComponent.Name + "@" + topLevelComponent.Id);
                node.Tag = topLevelComponent.Id;

                AddChildren(topLevelComponent, node);
            }

            MainView.ExpandAll();
        }

        private void AddChildren(Component baseComponent, TreeNode node)
        {
            foreach (var childLink in baseComponent.Parts)
            {
                var repo = new ComponentsRepo();
                var child = repo.Components
                    .FindIncluded(c => c.Id == childLink.ChildComponentId);                

                if (child != null)
                {
                    var childNode = node.Nodes.Add(child.Name + "@" + child.Id + " (" + childLink.Quantity + " шт.)");
                    childNode.Tag = child.Id;
                    AddChildren(child, childNode);
                }
            }
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Control && e.Shift)
            {
                NewTopLevelComponentButton_Click(sender, e);
                return;
            }

            if (e.KeyCode == Keys.N && e.Control)
            {
                NewEmbeddedComponentButton_Click(sender, e);
                return;
            }

            if (e.KeyCode == Keys.F2)
            {
                RenameComponent_Click(sender, e);
                return;
            }

            if (e.KeyCode == Keys.Delete)
            {
                RemoveComponent_Click(sender, e);
                return;
            }

            if (e.KeyCode == Keys.R && e.Control)
            {
                CreateOpenXmlReport_Click(sender, e);
                return;
            }

            if (e.KeyCode == Keys.D1 && e.Control)
            {
                DatabaseSeeder.SeedData1();
                RefreshView();
                return;
            }

            if (e.KeyCode == Keys.D2 && e.Control)
            {
                DatabaseSeeder.SeedData2();
                RefreshView();
            }

            if (e.KeyCode == Keys.D3 && e.Control)
            {
                DatabaseSeeder.SeedData3();
                RefreshView();
            }
        }

        private void NewTopLevelComponentButton_Click(object sender, EventArgs e)
        {
            string input = "";
            var result = InputDialogs.String(ref input, "Введите имя компонента");
            if (result == DialogResult.OK)
            {
                var repo = new ComponentsRepo();

                var componentExists = repo.Components.Count(c => c.Name == input) > 0;
                if (componentExists)
                {
                    MessageBox.Show("Компонент уже существует", "Ошибка");
                }
                else
                {
                    var newComponent = new Component { Name = input, IsTopLevel = true };
                    repo.Components.Add(newComponent);

                    RefreshView();
                }
            }
        }

        private void NewEmbeddedComponentButton_Click(object sender, EventArgs e)
        {
            var selectedNode = MainView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Ни один компонент не выбран.", "Ошибка");
                return;
            }

            string input = "";
            int componentId;
            decimal quantity = 0;

            var repo = new ComponentsRepo();

            var embeddedComponents = repo.Components.FindAll(c => !c.IsTopLevel).ToList();

            var selectedComponentId = (int)selectedNode.Tag;
            var selectedComponent = repo.Components
                .Find(c => c.Id == selectedComponentId);

            DialogResult result;
            bool circularLinkFound = false;

            do
            {
                componentId = -1; 

                result = InputDialogs.ComponentNameAndQuantity(
                    ref input, ref componentId, ref quantity,
                    "Введите имя компонента и количество", embeddedComponents);

                if (result == DialogResult.OK)
                {
                    circularLinkFound = componentId != -1 && CheckUplinks(selectedComponentId, componentId);
                }

                if ((result == DialogResult.OK) && circularLinkFound)
                {
                    MessageBox.Show("Обнаружено рекурсиное вложение компонентов", "Ошибка");
                }

            } while (result == DialogResult.OK && circularLinkFound);
            
            if (result == DialogResult.OK)
            {   
                ComponentLink existingLink = null;

                Component newComponent;

                if (componentId == -1)
                {
                    newComponent = new Component { Name = input };
                    repo.Components.Add(newComponent);
                }
                else
                {
                    newComponent = repo.Components.Find(c => c.Id == componentId);

                    existingLink = repo.ComponentLinks
                        .Find(cl =>
                            cl.ParentComponentId == selectedComponentId &&
                            cl.ChildComponentId == componentId);
                }

                if (existingLink != null)
                {
                    existingLink.Quantity += (int)quantity;
                }
                else
                {
                    if ((selectedComponent != null) && (newComponent != null))
                    {
                        var newLink = new ComponentLink
                        {
                            ParentComponentId = selectedComponent.Id,
                            ChildComponentId = newComponent.Id,
                            Quantity = (int)quantity
                        };
                        repo.ComponentLinks.Add(newLink);
                    }
                }                

                RefreshView();
            }
        }

        private bool CheckUplinks(int selectedComponentId, int componentId)
        {
            var repo = new ComponentsRepo();

            var uplinks = repo.ComponentLinks
                    .FindAllIncluded(cl => cl.ChildComponentId == selectedComponentId)
                    .ToList();

            var result = false;
            foreach (var uplink in uplinks)
            {
                if (uplink.ParentComponentId == componentId)
                {
                    result = true;
                    break;
                }

                if (!uplink.ParentComponent.IsTopLevel)
                {
                    var recursiveResult = CheckUplinks(uplink.ParentComponent.Id, componentId);
                    if (recursiveResult)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private void RenameComponent_Click(object sender, EventArgs e)
        {
            var selectedNode = MainView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Ни один компонент не выбран.", "Ошибка");
                return;
            }

            var repo = new ComponentsRepo();

            var selectedNodeId = (int)selectedNode.Tag;
            var selectedComponent = repo.Components
                .Find(c => c.Id == selectedNodeId);

            DialogResult result;
            bool componentNameExistsInDatabase;
            string input = "";

            do
            {
                if (selectedComponent != null)
                {
                    input = selectedComponent.Name;
                }

                result = InputDialogs.String(ref input, "Введите новое имя");

                var componentByName = repo.Components.
                    Find(c => c.Name == input);

                componentNameExistsInDatabase = (componentByName != null);

                if ((result == DialogResult.OK) && componentNameExistsInDatabase)
                {
                    MessageBox.Show("Компонент с таким именем (" +
                        componentByName.Name + "@" + componentByName.Id + ") уже существует в базе",
                        "Ошибка");
                }
            } while (result == DialogResult.OK && componentNameExistsInDatabase);

            if (result == DialogResult.OK)
            {
                if (selectedComponent != null)
                {
                    selectedComponent.Name = input;

                    repo.Components.Update(selectedComponent, selectedComponent.Id);
                }
                

                RefreshView();
            }
        }

        private void RemoveComponent_Click(object sender, EventArgs e)
        {
            var selectedNode = MainView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Ни один компонент не выбран.", "Ошибка");
                return;
            }

            var repo = new ComponentsRepo();

            var selectedNodeId = (int)selectedNode.Tag;
            var selectedComponent = repo.Components.Find(c => c.Id == selectedNodeId);
            if (selectedComponent == null)
            {
                return;
            }

            if (!selectedComponent.IsTopLevel)
            {
                var parentNodeId = (int)selectedNode.Parent.Tag;
                var parentComponent = repo.Components.Get(parentNodeId);

                var result = InputDialogs.RemoveChoice(
                    "Хотите ли вы разорвать связь между текущим компонентом (" +
                    selectedComponent + ") и его родителем (" +
                    parentComponent + ") " +
                    "или только удалить текущий узел?");

                if (result == DialogResult.Cancel)
                {
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    var linkToBroke = repo.ComponentLinks
                        .Find(cl =>
                            cl.ParentComponentId == parentComponent.Id &&
                            cl.ChildComponentId == selectedComponent.Id
                        );

                    if (linkToBroke != null)
                    {
                        repo.ComponentLinks.Delete(linkToBroke);
                    }
                }
            }

            RemoveSubComponents(selectedComponent);

            var componentCount = GetComponentCountInTree(selectedComponent);

            if (componentCount == 1)
            {
                var uplink = repo.ComponentLinks
                    .Find(cl => cl.ChildComponentId == selectedComponent.Id);
                if (uplink != null)
                {
                    repo.ComponentLinks.Delete(uplink);
                }
                repo.Components.Delete(selectedComponent);
            }
            else
            {
                TreeNode parentNode = selectedNode.Parent;
                var componentsChain = new List<Component>();
                do
                {
                    var parentComponent = repo.Components.Get((int)parentNode.Tag);
                    var parentCount = GetComponentCountInTree(parentComponent);

                    componentsChain.Add(parentComponent);

                    if (parentCount == 1)
                    {
                        break;
                    }

                    parentNode = parentNode.Parent;
                } while (true);

                var newComponentsChain = new List<Component>();
                for (int i = 0; i < componentsChain.Count - 1; i++)
                {
                    var oldComponent = componentsChain[i];

                    var newComponent = new Component
                    {
                        Name = oldComponent.Name,
                        IsTopLevel = oldComponent.IsTopLevel
                    };
                    repo.Components.Add(newComponent);
                    newComponentsChain.Add(newComponent);
                }

                if (componentsChain.Count == 1)
                {
                    var parentId = componentsChain[0].Id;
                    var childId = selectedComponent.Id;
                    var link = repo.ComponentLinks
                            .Find(cl =>
                                cl.ParentComponentId == parentId &&
                                cl.ChildComponentId == childId
                            );
                    if (link != null)
                    {
                        repo.ComponentLinks.Delete(link);
                    }
                }
                else
                {
                    var chainRootId = componentsChain[componentsChain.Count - 1].Id;
                    var chainFirstChildId = componentsChain[componentsChain.Count - 2].Id;
                    var linkToUpdate = repo.ComponentLinks.
                            Find(cl =>
                                cl.ParentComponentId == chainRootId &&
                                cl.ChildComponentId == chainFirstChildId);
                    if (linkToUpdate != null)
                    {
                        linkToUpdate.ChildComponentId = newComponentsChain[newComponentsChain.Count - 1].Id;
                    }

                    for (int i = componentsChain.Count - 1; i >= 1; i--)
                    {
                        if (i != componentsChain.Count - 1)
                        {
                            var oldLinkParentId = componentsChain[i].Id;
                            var oldLinkChildId = componentsChain[i - 1].Id;

                            var oldLink = repo.ComponentLinks
                                .Find(cl =>
                                    cl.ParentComponentId == oldLinkParentId &&
                                    cl.ChildComponentId == oldLinkChildId
                                );

                            var oldLinkQuantity = -1;
                            if (oldLink != null)
                            {
                                oldLinkQuantity = oldLink.Quantity;
                            }

                            var newChainLink = new ComponentLink
                            {
                                ParentComponentId = newComponentsChain[i].Id,
                                ChildComponentId = newComponentsChain[i - 1].Id,
                                Quantity = oldLinkQuantity
                            };
                            repo.ComponentLinks.Add(newChainLink);
                        }

                        var restoreExceptionId =
                            (i == 1) ? selectedComponent.Id :
                            componentsChain[i - 2].Id;

                        var oldParentId = componentsChain[i - 1].Id;
                        var oldChildrenLinks = repo.ComponentLinks
                            .FindAll(cl => cl.ParentComponentId == oldParentId &&
                                cl.ChildComponentId != restoreExceptionId)
                            .ToList();
                        foreach (var oldChildLink in oldChildrenLinks)
                        {
                            var restoredOldLink = new ComponentLink
                            {
                                ParentComponentId = newComponentsChain[i - 1].Id,
                                ChildComponentId = oldChildLink.ChildComponentId,
                                Quantity = oldChildLink.Quantity
                            };
                            repo.ComponentLinks.Add(restoredOldLink);
                        }
                    }
                }
            }

            RefreshView();
        }

        private int GetComponentCountInTree(Component needle)
        {
            var sum = 0;

            var repo = new ComponentsRepo();

            var topComponents = repo.Components.FindAll(c => c.IsTopLevel).ToList();

            foreach (var component in topComponents)
            {
                sum += GetSubcount(component, needle);
            }

            return sum;
        }

        private int GetSubcount(Component component, Component needle)
        {
            var sum = 0;

            var repo = new ComponentsRepo();

            var subComponents = repo.ComponentLinks.
                    FindAllIncluded(cl => cl.ParentComponentId == component.Id)                    
                    .Select(cl => cl.ChildComponent)
                    .ToList();
            foreach (var subComponent in subComponents)
            {
                sum += GetSubcount(subComponent, needle);
            }

            if (component.Id == needle.Id)
            {
                sum++;
            }

            return sum;
        }
        
        private void RemoveSubComponents(Component parentComponent)
        {
            var repo = new ComponentsRepo();

            var children = repo.ComponentLinks
                    .FindAllIncluded(cl => cl.ParentComponentId == parentComponent.Id)                    
                    .Select(cl => cl.ChildComponent)
                    .ToList();

            foreach (var child in children)
            {
                RemoveSubComponents(child);

                var componentCount = GetComponentCountInTree(child);

                if (componentCount == 1)
                {
                    var uplink = repo.ComponentLinks
                        .Find(cl => cl.ChildComponentId == child.Id);
                    if (uplink != null)
                    {
                        repo.ComponentLinks.Delete(uplink);
                    }
                    repo.Components.Delete(child);
                }
                else
                {
                    var parentCount = GetComponentCountInTree(parentComponent);
                    if (parentCount == 1)
                    {
                        var linkToBroke = repo.ComponentLinks
                            .Find(cl => cl.ParentComponentId == parentComponent.Id &&
                                cl.ChildComponentId == child.Id);
                        if (linkToBroke != null)
                        {
                            repo.ComponentLinks.Delete(linkToBroke);
                        }
                    }
                }
            }
        }

        private void CreateOpenXmlReport_Click(object sender, EventArgs e)
        {
            var selectedNode = MainView.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Ни один компонент не выбран.", "Ошибка");
                return;
            }

            WordExport.ExportOpenXmlReport(selectedNode);
        }                        
    }
}
