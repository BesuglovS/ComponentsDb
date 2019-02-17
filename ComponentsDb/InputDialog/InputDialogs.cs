using ComponentsDb.DomainClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ComponentsDb.InputDialog
{
    public static class InputDialogs
    {
        public static DialogResult String(ref string input, string caption)
        {
            Size size = new Size(200, 70);
            Form inputBox = new Form();

            inputBox.Load += InputBoxOnLoad;
            
            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ClientSize = size;
            inputBox.Text = caption;

            TextBox textBox = new TextBox
            {
                Size = new Size(size.Width - 10, 23),
                Location = new Point(5, 5),
                Text = input
            };
            inputBox.Controls.Add(textBox);

            Button okButton = new Button
            {
                DialogResult = DialogResult.OK,
                Name = "okButton",
                Size = new Size(75, 23),
                Text = "&OK",
                Location = new Point(size.Width - 80 - 80, 39)
            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button
            {
                DialogResult = DialogResult.Cancel,
                Name = "cancelButton",
                Size = new Size(75, 23),
                Text = "&Отмена",
                Location = new Point(size.Width - 80, 39)
            };
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            
            textBox.SelectionStart = textBox.Text.Length;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        public static DialogResult RemoveChoice(string message)
        {
            Size size = new Size(200, 120);
            Form inputBox = new Form();

            inputBox.Load += InputBoxOnLoad;

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ClientSize = size;
            inputBox.Text = "Удаление элемента";

            Label textBox = new Label
            {
                Size = new Size(size.Width - 10, 73),
                Location = new Point(5, 5),
                MaximumSize = new Size(size.Width - 10, 0),
                AutoSize = true,
                Text = message
            };
            inputBox.Controls.Add(textBox);

            Button okButton = new Button
            {
                DialogResult = DialogResult.Yes,
                Name = "okButton",
                Size = new Size(75, 23),
                Text = "Разорвать",
                Location = new Point(size.Width - 80 - 80, 89)
            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button
            {
                DialogResult = DialogResult.No,
                Name = "cancelButton",
                Size = new Size(75, 23),
                Text = "Удалить",
                Location = new Point(size.Width - 80, 89)
            };
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            return result;
        }

        private static void InputBoxOnLoad(object sender, EventArgs e)
        {
            ((Form)sender).SetDesktopLocation(Cursor.Position.X - 105, Cursor.Position.Y - 80);
        }

        public static DialogResult ComponentNameAndQuantity(ref string input, ref int componentId, ref decimal qty, string caption,
            List<Component> components)
        {
            Size size = new Size(300, 70);
            Form inputBox = new Form();

            inputBox.Load += InputBox2OnLoad;

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.MaximizeBox = false;
            inputBox.MinimizeBox = false;
            inputBox.ClientSize = size;
            inputBox.Text = caption;

            ComboBox comboBox = new ComboBox
            {
                Size = new Size(size.Width - 60, 23),
                Location = new Point(5, 5),
                Text = input,
                DataSource = components
            };

            inputBox.Controls.Add(comboBox);

            NumericUpDown quantity = new NumericUpDown
            {
                Size = new Size(40, 23),
                Location = new Point(255, 5),
                Minimum = 1,
                Maximum = 1000000
            };
            inputBox.Controls.Add(quantity);

            Button okButton = new Button
            {
                DialogResult = DialogResult.OK,
                Name = "okButton",
                Size = new Size(75, 23),
                Text = "&OK",
                Location = new Point(size.Width - 80 - 80, 39)
            };
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button
            {
                DialogResult = DialogResult.Cancel,
                Name = "cancelButton",
                Size = new Size(75, 23),
                Text = "&Отмена",
                Location = new Point(size.Width - 80, 39)
            };
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();

            if (comboBox.SelectedItem == null)
            {
                input = comboBox.Text;
                componentId = -1;
            }
            else
            {
                var component = (Component)comboBox.SelectedItem;
                input = component.Name;
                componentId = component.Id;
            }
            
            qty = quantity.Value;
            return result;
        }

        private static void InputBox2OnLoad(object sender, EventArgs e)
        {
            ((Form)sender).SetDesktopLocation(Cursor.Position.X - 210, Cursor.Position.Y - 80);
        }
    }
}
