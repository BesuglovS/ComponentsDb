using ComponentsDb.Context;
using ComponentsDb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ComponentsDb.Repositories;

namespace ComponentsDb.OpenXml
{
    public static class WordExport
    {
        public static void ExportOpenXmlReport(TreeNode selectedNode)
        {
            Component selectedComponent;
            string[,] tableData;

            var selectedNodeId = (int)selectedNode.Tag;

            var repo = new ComponentsRepo();
            selectedComponent = repo.Components.Find(c => c.Id == selectedNodeId);

            var contents = new Dictionary<int, Tuple<string, int>>();

            AddSubcomponents(ref contents, selectedComponent, 1);

            tableData = new string[contents.Count, 2];

            var ids = contents.Keys.OrderBy(id => id).ToList();
            for (int i = 0; i < ids.Count; i++)
            {
                var id = ids[i];
                var componentData = contents[id];

                tableData[i, 0] = componentData.Item1;
                tableData[i, 1] = componentData.Item2.ToString() + " шт";
            }

            if (selectedComponent != null)
            {
                string filename = selectedComponent.Id + "@" + selectedComponent.Name + ".docx";
                CreateOpenXmlFile(filename);
                AddTable(filename, tableData);

                System.Diagnostics.Process.Start(filename);
            }
        }

        public static void AddSubcomponents(ref Dictionary<int, Tuple<string, int>> contents, Component parentComponent,
            int multiplier)
        {
            var repo = new ComponentsRepo();

            var childrenLinks = repo.ComponentLinks
                    .FindAllIncluded(cl => cl.ParentComponentId == parentComponent.Id)
                    .ToList();

            foreach (var childLink in childrenLinks)
            {
                var child = repo.Components.Get(childLink.ChildComponentId);

                if (child.Parts.Count == 0)
                {
                    if (!contents.ContainsKey(child.Id))
                    {
                        contents.Add(child.Id, new Tuple<string, int>(child.Name, childLink.Quantity * multiplier));
                    }
                    else
                    {
                        contents[child.Id] = new Tuple<string, int>(child.Name, contents[child.Id].Item2 + childLink.Quantity * multiplier);
                    }
                }
                else
                {
                    AddSubcomponents(ref contents, child, multiplier * childLink.Quantity);
                }
            }
        }

        public static void AddTable(string filename, string[,] data)
        {
            using (var document = WordprocessingDocument.Open(filename, true))
            {

                var doc = document.MainDocumentPart.Document;

                Table table = new Table();

                TableProperties props = new TableProperties(
                    new TableBorders(
                    new TopBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 12
                    }));

                table.AppendChild<TableProperties>(props);

                for (var i = 0; i <= data.GetUpperBound(0); i++)
                {
                    var tr = new TableRow();
                    for (var j = 0; j <= data.GetUpperBound(1); j++)
                    {
                        var tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(data[i, j]))));

                        var width = 50 / data.GetUpperBound(1);
                        tc.Append(new TableCellProperties(
                            new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = width.ToString() }));

                        tr.Append(tc);
                    }
                    table.Append(tr);
                }
                doc.Body.Append(table);
                doc.Save();
            }
        }

        public static void CreateOpenXmlFile(string filename)
        {
            using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(filename, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                mainPart.Document = new Document();
                mainPart.Document.AppendChild(new Body());
            }
        }
    }
}
