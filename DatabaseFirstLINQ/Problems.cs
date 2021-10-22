using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var users = _context.Users.ToList().Count();
            Console.WriteLine(users);

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Price > 150).ToList();
            foreach(var product in products)
            {
                Console.WriteLine(product.Name);
            }


        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products.Where(p => p.Name.Contains("s"));
            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            DateTime date = Convert.ToDateTime("01/01/2016");
            var users = _context.Users.Where(u => u.RegistrationDate < date);
            foreach (var user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var date = DateTime.Parse("01/01/2016");
            var dateTwo = DateTime.Parse("01/01/2018");
            var users = _context.Users.Where(u => u.RegistrationDate > date && u.RegistrationDate < dateTwo);
            foreach (var user in users)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var user = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "afton@gmail.com");
            foreach(var u in user)
            {
                Console.WriteLine(u.Product.Name + " " + u.Quantity + " " + u.Product.Price);
            }


        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var ShoppingCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(ShoppingCart);

        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

           
            var employeeIds = _context.UserRoles.Where(e => e.RoleId == 2).Select(e => e.UserId);
            var employeeShoppingCarts = _context.ShoppingCarts.Include(e => e.User).Include(e => e.Product).Where(e => employeeIds.Contains(e.UserId));
            foreach(var e in employeeShoppingCarts)
            {
                Console.WriteLine(e.User.Email + " " + e.Product.Name + " " + e.Product.Price + " " + e.Quantity);
            }





        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "PS5",
                Description = "Gaming console",
                Price = 400
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Name == "PS5").Select(p => p.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "PS5").SingleOrDefault();
            product.Price = 500;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Please input your email: ");
            string userEmail = Console.ReadLine();
            Console.WriteLine("PW: ");
            string userPW = Console.ReadLine();
            var user = _context.Users.Where(u => u.Email == userEmail).SingleOrDefault();
            if (user != null)
            {
                Console.WriteLine(user.Email + " is signed in!");
            }
            else
            {
                Console.WriteLine("There is no user with that email!");
            }

        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
            var users = _context.Users.ToArray();
            var shoppingCarts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User);
            var cartIndex = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).FirstOrDefault();
            List<decimal> totalList = new List<decimal>();
            decimal total = 0;

            for(int i = 0; i < users.Length; i++)
            {
                var userShoppingCart = (decimal)_context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == users[i].Email).Select(sc => sc.Product.Price * sc.Quantity).Sum();
                totalList.Add(userShoppingCart);
               
            }
            foreach(var list in totalList)
            {
                Console.WriteLine(list);
                total += list;
            }
            Console.WriteLine("Total: " + total);

            

        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

          

            Console.WriteLine("Please input your email: ");
            string userEmail = Console.ReadLine();
            Console.WriteLine("PW: ");
            string userPW = Console.ReadLine();
            var user = _context.Users.Where(u => u.Email == userEmail && u.Password == userPW).SingleOrDefault();

            bool signedIn = false;
            if (user != null)
            {
                signedIn = true;
                Console.WriteLine("Successful sign in!");
            }
            else
            {
                Console.WriteLine("Invalid email/password");
            }
            
            while (!signedIn)
            {
                Console.WriteLine("Please input your email: ");
                userEmail = Console.ReadLine();
                Console.WriteLine("PW: ");
                userPW = Console.ReadLine();
                user = _context.Users.Where(u => u.Email == userEmail && u.Password == userPW).SingleOrDefault();
                if (user != null)
                {
                    Console.WriteLine(user.Email + " is signed in!");
                    signedIn = true;
                }
                else
                {
                    Console.WriteLine("Invalid email/password");
                    
                }
            }
           
            
            
            bool sessionOpen = true;
            while (sessionOpen)
            {
                Console.WriteLine("\n1: View Cart\n2: View Products\n3: Add Product\n4: Delete Item from Cart\n5: Quit\n\nPlease enter selection: ");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        var userCart = _context.ShoppingCarts.Include(uc => uc.Product).Where(uc => uc.User.Email == userEmail);
                        foreach (var item in userCart)
                        {
                            Console.WriteLine("Product: " + item.Product.Name + " Quantity: " + item.Quantity);
                        }
                        break;
                    case "2":
                        var products = _context.Products;
                        foreach (var product in products)
                        {
                            Console.WriteLine(product.Name);
                        }
                        break;
                    case "3":
                        Console.WriteLine("\nWhich item would you like to add?");
                        var productsArray = _context.Products.ToArray();
                        for (int i = 0; i < productsArray.Length; i++)
                        {
                            Console.WriteLine((i + 1) + ": " + productsArray[i].Name);
                        }
                        var itemInput = int.Parse(Console.ReadLine());
                        var userCartId = _context.ShoppingCarts.Include(uc => uc.Product).Where(uc => uc.User.Email == userEmail).Select(uc => uc.ProductId);
                        Console.WriteLine("\nHow many would you like to add?");
                        var quantity = int.Parse(Console.ReadLine());

                        if (userCartId.Contains(itemInput))
                        {
                            var itemCart = _context.ShoppingCarts.Where(i => i.UserId == user.Id && i.ProductId == itemInput).SingleOrDefault();
                            itemCart.Quantity += quantity;
                            _context.ShoppingCarts.Update(itemCart);
                            _context.SaveChanges();
                            Console.WriteLine("\nItem updated");
                        }
                        else
                        {
                            ShoppingCart newShoppingCart = new ShoppingCart()
                            {
                                UserId = user.Id,
                                ProductId = itemInput,
                                Quantity = quantity
                            };
                            _context.ShoppingCarts.Add(newShoppingCart);
                            _context.SaveChanges();
                            Console.WriteLine("\nItem(s) added");
                        }
                        break;
                    case "4":
                        var userCartArray = _context.ShoppingCarts.Include(uc => uc.Product).Where(uc => uc.User.Email == userEmail).ToArray();
                        Console.WriteLine("Which item would you like to remove? (enter item ID)");
                        for (int i = 0; i < userCartArray.Length; i++)
                        {
                            Console.WriteLine(userCartArray[i].ProductId + ": " + userCartArray[i].Product.Name);
                        }
                        var itemIdToRemove = int.Parse(Console.ReadLine());
                        var ItemToRemove = _context.ShoppingCarts.Where(itr => itr.UserId == user.Id && itr.ProductId == itemIdToRemove).SingleOrDefault();
                        _context.ShoppingCarts.Remove(ItemToRemove);
                        _context.SaveChanges();
                        Console.WriteLine("Item successfully removed!");
                        break;
                    case "5":
                        Console.WriteLine("Thank you for using our application!");
                        sessionOpen = false;
                        signedIn = false;
                        break;

                }
            }

            


            
            

            





        }

    }
}
