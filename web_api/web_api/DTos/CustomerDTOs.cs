using MongoDB.Bson;

namespace web_api.DTos
{
   public record CreatCustomer(string name,string email,string contactNumber);
    public record UpdateCustomer(string id,string name, string email, string contactNumer,bool IsActive);
}
