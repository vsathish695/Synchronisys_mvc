using Synchronisys_mvc.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace Synchronisys_mvc.Controllers
{
    public class UserController : ApiController
    {
        clsDatabase lobjDatabase = new clsDatabase();
        // GET: User list
        public HttpResponseMessage GET(int Page = 1)
        {
            Page = Page == 0 ? 1 : Page;
            mdlUser user = new mdlUser();
            user.page = Page;
            int skip = 0;
            int take = 3;
            if (Page > 1)
            {
                skip = take * (Page - 1);
            }
             
            user.per_page = take;
            user.total = lobjDatabase.User.ToList().Count;
            List<UserDetails> userlist = lobjDatabase.User.AsQueryable().OrderBy(x => x.id).Skip(skip).Take(take).ToList();
            user.data = userlist;
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        // GET: User by id
        [Route("api/user/{id}")]
        public HttpResponseMessage GETUser(int id = 1)
        {
            UserDetails user = new UserDetails();
            UserDetails userlist = lobjDatabase.User.Find(id);
            user = userlist;
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        //POST: ADD USER
        public HttpResponseMessage POST(UserDetails user)
        {
            StatusDesc statusDesc = new StatusDesc();
            if (user == null)
            {
                statusDesc.StatusCode = 0;
                statusDesc.Description = "User details cannot be null";
                return Request.CreateResponse(HttpStatusCode.OK, statusDesc);
            }
            if (!ModelState.IsValid)
            {
                string error = ModelState.SelectMany(state => state.Value.Errors).Aggregate("", (a, b) => a + "," + b.ErrorMessage.ToString());
                statusDesc.StatusCode = 0;
                statusDesc.Description = error;
                return Request.CreateResponse(HttpStatusCode.OK, statusDesc);
            }
            user.avatar = "https://s3.amazonaws.com/uifaces/faces/twitter/marcoramires/128.jpg";
            lobjDatabase.User.Add(user);
            int returnvalue = lobjDatabase.SaveChanges();

            statusDesc.StatusCode = returnvalue;
            statusDesc.Description = returnvalue == 1 ? "User added successfully" : "Unable to add user";
            return Request.CreateResponse(HttpStatusCode.OK, statusDesc);
        }

        //PUT: EDIT USER
        public HttpResponseMessage PUT(UserDetails user)
        {
            StatusDesc statusDesc = new StatusDesc();
            UserDetails ExistingRecord = lobjDatabase.User.AsNoTracking().FirstOrDefault(x => x.id == user.id);
            int userId = ExistingRecord.id;
            ExistingRecord = user;
            ExistingRecord.id = userId;
            lobjDatabase.User.Attach(ExistingRecord);
            lobjDatabase.Entry(ExistingRecord).State = EntityState.Modified;
            int returnvalue = lobjDatabase.SaveChanges();

            statusDesc.StatusCode = returnvalue;
            statusDesc.Description = returnvalue == 1 ? "User updated successfully" : "Unable to update user";
            return Request.CreateResponse(HttpStatusCode.OK, statusDesc);
        }
        //DELETE: DELETE USER
        public HttpResponseMessage DELETE(int Id)
        {
            StatusDesc statusDesc = new StatusDesc();
            UserDetails lobjUser = lobjDatabase.User.FirstOrDefault(x => x.id == Id);
            lobjDatabase.User.Remove(lobjUser);
            int returnvalue = lobjDatabase.SaveChanges();

            statusDesc.StatusCode = returnvalue;
            statusDesc.Description = returnvalue == 1 ? "User deleted successfully" : "Unable to delete user";
            return Request.CreateResponse(HttpStatusCode.OK, statusDesc);
        }
    }
}