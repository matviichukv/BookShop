using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfBookShopService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBookShopService
    {
        [OperationContract]
        IList<string> GetBooks();

        [OperationContract]
        void RegisterUser(string name, string email, string password);

        [OperationContract]
        IList<string> GetBooksByCategory(string category);

        [OperationContract]
        string GetUserInfo(int userId);

        [OperationContract]
        IList<string> GetCategories();

        [OperationContract]
        void BuyBook(int bookId);

        [OperationContract]
        bool CheckUserCredentials(string email, string password);

        [OperationContract]
        DbSet GetDbSetByType(Type type);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";
    //
    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }
    //
    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}
}
