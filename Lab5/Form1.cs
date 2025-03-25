using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private List<Building> buildings = new List<Building>();
        public Form1()
        {
            InitializeComponent();
            InitializeBuildings();
            PopulateTreeView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeBuildings()
        {
            buildings.Add(new Building("Building 1", 10, 45, new List<string> { "Company A", "Company B" }));
            buildings.Add(new Building("Building 2", 15, 65, new List<string> { "Hotel A", "Office Space A" }));
            buildings.Add(new Building("Building 3", 5, 25, new List<string> { "Company C", "Restaurant A" }));
        }

        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();

            foreach (Building building in buildings)
            {
                TreeNode buildingNode = new TreeNode(building.Name);

                buildingNode.Nodes.Add($"Floors: {building.Floors}");
                buildingNode.Nodes.Add($"Height: {building.Height}m");

                TreeNode tenantsNode = new TreeNode("Tenants");
                foreach (string tenant in building.Tenants)
                {
                    tenantsNode.Nodes.Add(new TreeNode(tenant));
                }

                buildingNode.Nodes.Add(tenantsNode);
                treeView1.Nodes.Add(buildingNode);
            }

            treeView1.ExpandAll(); 
        }

        public void DisplayBuildingInTreeView(Building building, TreeView treeView)
        {
            treeView.Nodes.Clear();

            TreeNode rootNode = new TreeNode($"Building: {building.Name}");
            PropertyInfo[] properties = typeof(Building).GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                object value = prop.GetValue(building);
                string typeName = prop.PropertyType.Name;

                if (value is List<string> tenantsList) 
                {
                    TreeNode tenantsNode = new TreeNode($"Tenants (List<string>):");
                    foreach (var tenant in tenantsList)
                    {
                        tenantsNode.Nodes.Add(new TreeNode(tenant));
                    }
                    rootNode.Nodes.Add(tenantsNode);
                }
                else
                {
                    rootNode.Nodes.Add(new TreeNode($"{prop.Name} ({typeName}): {value}"));
                }
            }

            treeView.Nodes.Add(rootNode);
            treeView.ExpandAll();
        }

        private void buttonDetailedDisplay_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                TreeNode selectedNode = treeView1.SelectedNode;
                while (selectedNode.Parent != null)
                {
                    selectedNode = selectedNode.Parent;
                }

                string buildingName = selectedNode.Text; 

                Building selectedBuilding = buildings.FirstOrDefault(b => b.Name == buildingName);

                if (selectedBuilding != null)
                {
                    DisplayBuildingInTreeView(selectedBuilding, treeView1);
                }
                else
                {
                    MessageBox.Show("Building not found.");
                }
            }
            else
            {
                MessageBox.Show("Please select a building.");
            }
        }

        private void buttonRestoreDisplay_click(object sender, EventArgs e)
        {
            PopulateTreeView();
        }

        private void btnAddTenant(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                string tenantName = textTenantName.Text.Trim();
                if (!string.IsNullOrEmpty(tenantName))
                {
                    Building selectedBuilding = buildings.FirstOrDefault(b => b.Name == treeView1.SelectedNode.Text);
                    if (selectedBuilding != null)
                    {
                        selectedBuilding.AddTenant(tenantName);
                        PopulateTreeView(); 
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a tenant name.");
                }
            }
            else
            {
                MessageBox.Show("Please select a building first.");
            }
        }

        private void btnRemoveTenant(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {
                string tenantName = treeView1.SelectedNode.Text;
              
                foreach (Building building in buildings)
                {
                    if (building.Tenants.Contains(tenantName))
                    {
                        building.RemoveTenant(tenantName);
                        PopulateTreeView(); 
                        return;
                    }
                }
                MessageBox.Show("Tenant not found in any building.");
            }
            else
            {
                MessageBox.Show("Please select a tenant to remove.");
            }
        }

        private void btnDisplayBuilding(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                Building selectedBuilding = buildings.FirstOrDefault(b => b.Name == treeView1.SelectedNode.Text);
                if (selectedBuilding != null)
                {
                    selectedBuilding.DisplayInfo();
                }
                else
                {
                    MessageBox.Show("Please select a building node.");
                }
            }
        }
    }

    public class Building
    {
        public string Name { get; set; }
        public int Floors { get; set; }
        public double Height { get; set; }
        public List<string> Tenants { get; set; }

        public Building()
        {
            Name = "Unknown";
            Floors = 1;
            Height = 3.0;
            Tenants = new List<string>();
        }

        public Building(string name, int floors, double height, List<string> tenants)
        {
            Name = name;
            Floors = floors;
            Height = height;
            Tenants = tenants ?? new List<string>();
        }

        public void AddTenant(string tenant)
        {
            if (!Tenants.Contains(tenant))
            {
                Tenants.Add(tenant);
                MessageBox.Show($"Tenant '{tenant}' added successfully.");
            }
            else
            {
                MessageBox.Show($"Tenant '{tenant}' already exists.");
            }
        }

        public void RemoveTenant(string tenant)
        {
            if (Tenants.Contains(tenant))
            {
                Tenants.Remove(tenant);
                MessageBox.Show($"Tenant '{tenant}' removed successfully.");
            }
            else
            {
                MessageBox.Show($"Tenant '{tenant}' not found.");
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Building: {Name}, Floors: {Floors}, Height: {Height}m");
            Console.WriteLine("Tenants: " + (Tenants.Count > 0 ? string.Join(", ", Tenants) : "No tenants"));
        }
    }
}
