using API.Context;
using API.Middleware;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class UserRepository : GeneralRepository<User, int>
    {
        MyContext myContext;

        public UserRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public UserRole GetRole(int id)
        {
            var getUserId = myContext.UserRoles.FirstOrDefault(x => x.User_Id == id);

            return getUserId;
        }

        public User GetUser(string email)
        {
            var getEmployee = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email));
            if (getEmployee != null)
            {
                var getUser = myContext.Users.Find(getEmployee.Id);
                return getUser;
            }
            return null;
        }

        /*public Employee GetEmployee(string email)
        {
            var getEmployee = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email));
            return getEmployee;
        }*/

        //LOGIN
        public User Login(Login login)
        {
            var data = GetUser(login.Email);
            if (data != null)
            {
                /*if (data.Password == login.Password)
                    return data;*/
                //var registeredUser = myContext.Users.SingleOrDefault(x => x.Employee.Email.Equals(register.Email));
                //bool validatePassword = BCrypt.Net.BCrypt.Verify(login.Password, data.Password);
                if (Hashing.ValidatePassword(login.Password, data.Password))
                {
                    return data;
                }
            }
            return null;
        }


        //REGISTER
        public int Register(Register register)
        {
            // preparation default role (get the lowest Level Role)
            //var level = myContext.Levels.Min(x => x.Value);
            //var roleDefault = myContext.Roles.SingleOrDefault(x => x.Level.Value.Equals(level)).Id;

            // check duplicate email
            /*var checking = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email));
            if (checking == null)
                return -1;*/

            if (myContext.Users.Any(x => x.Username == register.Username) || myContext.Employees.Any(x => x.Email == register.Email))
                return -1;

            // mapping Model Employee from Register
            Employee employee = new Employee()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                HireDate = register.HireDate,
                Salary = register.Salary,
                Job_Id = register.Job_Id,
                Department_Id = register.Department_Id
            };

            // insert data Employee to database
            myContext.Employees.Add(employee);
            var registeringEmployee = myContext.SaveChanges();

            // if inserted
            if (registeringEmployee > 0)
            {
                // -> preparation assigning role
                var registeredEmployee = myContext.Employees.SingleOrDefault(x => x.Email.Equals(register.Email)).Id;
                
                //var getRandomSalt = BCrypt.Net.BCrypt.GenerateSalt(12);

                //var password = Hashing.HashPassword(register.Password);

                // -> mapping Model User from Register
                User user = new User()
                {
                    Id = registeredEmployee,
                    Username = register.Username,
                    //Password = register.Password
                    // Password = BCrypt.Net.BCrypt.HashPassword(register.Password, getRandomSalt)
                    Password = Hashing.HashPassword(register.Password)
                };

                // -> insert data User to database
                myContext.Users.Add(user);
                var registeringUser = myContext.SaveChanges();

                // -> if inserted
                if (registeringUser > 0)
                {
                    // --> mapping Model UserRole
                    UserRole userRole = new UserRole();
                    userRole.Role_Id = 1;
                    userRole.User_Id = registeredEmployee;

                    // --> insert data UserRole to database
                    myContext.UserRoles.Add(userRole);
                    var assigningRole = myContext.SaveChanges();
                    if (assigningRole > 0)
                    {
                        return assigningRole;
                    }
                }
                else
                {
                    // else clear data registered
                    var dataEmployee = myContext.Employees.Find(registeredEmployee);
                    myContext.Employees.Remove(dataEmployee);
                    var deletingEmployee = myContext.SaveChanges();
                }
            }
            return 0;
        }


        //CHANGE PASSWORD
        public int ChangePassword(ChangePassword changePassword)
        {
            var data = myContext.Users.SingleOrDefault(x => x.Employee.Email.Equals(changePassword.Email));
            if (data != null)
            {
                var user = myContext.Users.Find(data.Id);
                if (user != null)
                {
                    if (changePassword.OldPassword == user.Password)
                    {
                        user.Password = changePassword.NewPassword;
                        int result = myContext.SaveChanges();
                        return result;
                    }
                }
            }
            return 0;
        }


        //FORGOT PASSWORD
        public int ForgotPassword(ForgotPassword forgotPassword)
        {
            var data = GetUser(forgotPassword.Email);
            if (data != null)
            {
                data.Password = forgotPassword.NewPassword;
                int result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
