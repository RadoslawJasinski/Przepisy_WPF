using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Przepisy_WPF.Models;

namespace Przepisy_WPF
{
    class DbConnect
    {
        private MySqlConnection cn;
        private string server;
        private string database;
        private string user;
        private string password;

        public List<Recipe> RecipesAllList { get; set; }
        public List<Recipe> SelectedRecipes { get; set; }
        public List<Recipe> BreakfastList { get; set; }
        public List<Recipe> DinnerList { get; set; }
        public List<Recipe> SnackList { get; set; }
        public List<Recipe> DessertList { get; set; }



        public DbConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "bglkg5fecnbvhysm4c8y-mysql.services.clever-cloud.com";
            database = "bglkg5fecnbvhysm4c8y";
            user = "uot0rqrg9upnfxem";
            password = "UaivAUSOg5fFvqhAi2RI";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";

            cn = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                cn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Bład połączenia z bazą danych");
                        break;

                    case 1045:
                        MessageBox.Show("Błędny login lub hasło");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                cn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<Recipe> GetData()
        {
            string id;
            string name;
            string description;
            string imageURL;
            string spices;
            string categoryID;
            string query = "SELECT * FROM Recipes";

            RecipesAllList = new List<Recipe>();

            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id = dr["id_recipe"].ToString();
                    name = dr["name_recipe"].ToString();
                    description = dr["description_recipe"].ToString();
                    imageURL = dr["imageURL_recipe"].ToString();
                    spices = dr["spices_recipe"].ToString();
                    categoryID = dr["id_category"].ToString();

                    RecipesAllList.Add(new Recipe(id,name, description, imageURL, spices, categoryID));
                }
                dr.Close();
                this.CloseConnection();
                BreakfastList = RecipesAllList.FindAll(x => x.CategoryID.Contains("1"));
                DinnerList = RecipesAllList.FindAll(x => x.CategoryID.Contains("2"));
                SnackList = RecipesAllList.FindAll(x => x.CategoryID.Contains("3"));
                DessertList = RecipesAllList.FindAll(x => x.CategoryID.Contains("4"));
                return RecipesAllList;
            }
            else
            {
                return RecipesAllList;
            }
        }

        public List<Ingredient> GetIngredient() //Get all ingredients from database
        {
            string id;
            string name;
            string query = "SELECT * FROM Ingredients";

            List<Ingredient> ingredient = new List<Ingredient>();

            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    id = dr["id_ingredient"].ToString();
                    name = dr["name_ingredient"].ToString();

                    ingredient.Add(new Ingredient(id, name));
                }
                dr.Close();
                this.CloseConnection();
                return ingredient;
            }
            else
            {
                return ingredient;
            }
        }

        public List<Ingredient> GetSelectedIngredientsName(string id_recipe) //Get all matching ingredient's name for clicked recipe by recipeID
        {
            string query = $"SELECT Ingredients.name_ingredient FROM Ingredients INNER JOIN Recipes_ingredients ON Recipes_ingredients.id_ingredient = Ingredients.id_ingredient WHERE Recipes_ingredients.id_recipe = {id_recipe}";
            string name;
            List<Ingredient> ingredientName = new List<Ingredient>();

            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    name = dr["name_ingredient"].ToString();

                    ingredientName.Add(new Ingredient(name));
                }
                dr.Close();
                this.CloseConnection();
                return ingredientName;
            }
            else
            {
                return ingredientName;
            }
        }

        public string GetSelectedRecipeID(string ingredientID) //This method returns the matching recipeID for the selected ingredients
        {
            string query = $"SELECT DISTINCT c.id_recipe FROM (SELECT id_recipe from Recipes_ingredients WHERE id_ingredient IN ({ingredientID})) " +
                $"c LEFT JOIN (SELECT id_recipe FROM Recipes_ingredients WHERE id_ingredient NOT IN ({ingredientID})) x ON c.id_recipe = x.id_recipe WHERE x.id_recipe IS NULL";
            string recipeID = "";

            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    recipeID += dr["id_recipe"].ToString() + ",";
                }
                dr.Close();
                this.CloseConnection();
                if (recipeID.Length > 0)
                {
                    recipeID = recipeID.Remove(recipeID.Length - 1);
                }
                return recipeID;
            }
            else
            {
                return recipeID;
            }
        }

        public List<Recipe> GetSelected(string recipeID) //Get more information about recipe by recipeID
        {
            string id;
            string name;
            string description;
            string imageURL;
            string spices;
            string categoryID;
            string query = $"SELECT * FROM Recipes WHERE id_recipe IN ({recipeID})";

            SelectedRecipes = new List<Recipe>();

            if (recipeID.Length > 0)
            {
                if (this.OpenConnection() == true)
                {
                    var cmd = new MySqlCommand(query, cn);
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        id = dr["id_recipe"].ToString();
                        name = dr["name_recipe"].ToString();
                        description = dr["description_recipe"].ToString();
                        imageURL = dr["imageURL_recipe"].ToString();
                        spices = dr["spices_recipe"].ToString();
                        categoryID = dr["id_category"].ToString();

                        SelectedRecipes.Add(new Recipe(id,name, description, imageURL, spices, categoryID));
                    }
                    dr.Close();
                    this.CloseConnection();
                    return SelectedRecipes;
                }
                else
                {
                    return SelectedRecipes;
                }
            }
            else
            {
                return SelectedRecipes;

            }
        }

        public List<Recipe> GetDetailImages(string recipe_id) //Get more images for clicked recipe
        {
            string imageURL;
            string query = $"SELECT * FROM Recipes_images WHERE id_recipe = {recipe_id}";

            var imagesList = new List<Recipe>();


            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    imageURL = dr["ImageURL"].ToString();

                   imagesList.Add(new Recipe(imageURL));
                }
                dr.Close();
                this.CloseConnection();
                return imagesList;
            }
            else
            {
                return imagesList;
            }
        }

        public List<Ingredient> GetDetailQuantityIngredient(string recipe_id) //Get quantity foreach ingredient 
        {
            string quantity;
            string id_recipe;
            string id_ingredient;

            string query = $"SELECT * FROM Recipes_ingredients WHERE id_recipe = {recipe_id}";

            var quantityList = new List<Ingredient>();


            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    quantity = dr["qu_ingredient"].ToString();
                    id_ingredient = dr["id_ingredient"].ToString();
                    id_recipe = dr["id_recipe"].ToString();


                    quantityList.Add(new Ingredient(quantity,id_ingredient,id_recipe));
                }
                dr.Close();
                this.CloseConnection();
                return quantityList;
            }
            else
            {
                return quantityList;
            }
        }


    }
}
