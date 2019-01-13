using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeWebApliccatin
{
    public class Phone
    {

        #region MyVAriables

        public static string connStr = ConfigurationManager.ConnectionStrings["ProductsWeb"].ConnectionString;

        private int? id = 1;
        public int? Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private Brands? brand = Brands.Samsung;
        public Brands? Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        private Boolean available;
        public Boolean Available
        {
            get { return available; }
            set { available = value; }
        }


        private DateTime? publishDate = null;
        public DateTime? PublishDate
        {
            get { return publishDate; }
            set { publishDate = value; }
        }

        private string imgUrl = "";
        public string ImgURL
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }

        private OsType? os = OsType.Android;
        public OsType? OS
        {
            get { return os; }
            set { os = value; }
        }

        private ProductType? prType = ProductType.Phone;
        public ProductType? PrType
        {
            get { return prType; }
            set { prType = value; }
        }


        private Boolean? sale;
        public Boolean? Sale
        {
            get { return sale; }
            set { sale = value; }
        }

        private float? price;
        public float? Price
        {
            get { return price; }
            set { price = value; }
        }

        private float? discountPrice;
        public float? DiscountPrice
        {
            get { return discountPrice; }
            set { discountPrice = value; }
        }
        private string summery = "";
        public string Summery
        {
            get { return summery; }
            set { summery = value; }
        }

        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int? mycount ;
        public int? MyCount
        {
            get { return mycount; }
            set { mycount = value; }
        }
        #endregion


        public static List<Phone> GetPhonesListByType(ProductType? myPrType)
        {
            
            List<Phone> phoneList = new List<Phone>();
          
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetPhonesListByType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            SqlDataReader dr;


            try
            {
                if (myPrType != null)
                {

                    param = new SqlParameter("PrType", myPrType);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("PrType", DBNull.Value));

                con.Open();
                dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Phone PH = new Phone();
                    PH.Id = Convert.ToInt32(dr["ID"]);
                    PH.Name = dr["Name"].ToString();
                    PH.Brand = (Brands)Convert.ToByte( dr["Brand"]);
                    PH.Available = Convert.ToBoolean(dr["Available"]);
                    if (dr["PublishDate"] == DBNull.Value)
                        PH.PublishDate = null;
                    else
                        PH.PublishDate = Convert.ToDateTime(dr["PublishDate"]);

                    if (dr["ImgUrl"] == DBNull.Value)
                        PH.ImgURL = null;
                    else
                        PH.ImgURL = dr["ImgUrl"].ToString();
                    PH.PrType = (ProductType)Convert.ToByte(dr["PrType"]);
                    PH.OS =(OsType)Convert.ToByte(dr["OS"]);
                    PH.Summery= dr["Summery"].ToString();
                    PH.Sale = Convert.ToBoolean(dr["Sale"]);
                    if (dr["Price"] == DBNull.Value)
                        PH.Price = null;
                    else
                        PH.Price = (float)(double)(dr["Price"]);

                    if (dr["DiscountPrice"] == DBNull.Value)
                        PH.DiscountPrice = null;
                    else
                        PH.DiscountPrice = (float)(double)(dr["DiscountPrice"]);
                    phoneList.Add(PH);
                }

                dr.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return phoneList;
        }

        public static List<Phone> GetDevicesCount()
        {

            List<Phone> DeviceList = new List<Phone>();

            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetDevicesCount", con);
            cmd.CommandType = CommandType.StoredProcedure;


            SqlDataReader dr;


            try
            {
               

                con.Open();
                dr = cmd.ExecuteReader();


                while (dr.Read())
                {
                    Phone Device = new Phone();

                    Device.PrType = (ProductType)Convert.ToByte(dr["PrType"]);
                  
                    if (dr["Mycount"] == DBNull.Value)
                        Device.MyCount = null;
                    else
                        Device.MyCount =Convert.ToInt32(dr["Mycount"]);

                    DeviceList.Add(Device);
                }

                dr.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return DeviceList;
        }

        public static Phone GetPhoneByID(int id)
        {

            Phone output = null;
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetPhoneByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            SqlDataReader dtr;

            try
            {
                param = new SqlParameter("Id", id);
                cmd.Parameters.Add(param);
                con.Open();
                dtr = cmd.ExecuteReader();

                while (dtr.Read())
                {

                    output = new Phone();
                    output.Id = (int)dtr["Id"];
                    output.Name = dtr["Name"].ToString();
                    output.brand =(Brands)Convert.ToByte(dtr["Brand"]);
                    output.available = Convert.ToBoolean(dtr["Available"]);

                    if (dtr["PublishDate"] == DBNull.Value)
                        output.publishDate = null;
                    else
                        output.publishDate = Convert.ToDateTime(dtr["PublishDate"]);

                    if (dtr["ImgUrl"] == DBNull.Value)
                        output.ImgURL = null;
                    else
                        output.ImgURL = dtr["ImgUrl"].ToString();
                    output.PrType = (ProductType)Convert.ToByte(dtr["PrType"]);
                    output.OS = (OsType)Convert.ToByte(dtr["OS"]);

                    if (dtr["Price"] == DBNull.Value)
                        output.price = null;
                    else
                        output.price = (float)(double)(dtr["Price"]);

                    if (dtr["DiscountPrice"] == DBNull.Value)
                        output.DiscountPrice = null;
                    else
                        output.DiscountPrice = (float)(double)(dtr["DiscountPrice"]);

                    //DBNull.Value ? null : Convert.ToDateTime(reader[3])
                    output.Summery = dtr["Summery"].ToString();
                    output.Description = dtr["Description"].ToString();
                    output.Sale = Convert.ToBoolean(dtr["Sale"]);
                }
                dtr.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return output;

        }

        public static List<Phone> GetPhoneListByFilter(int? id, string MyBrands, ProductType myType,string myName,string MyOSTypes, float? myMinPrice, float? myMaxPrice)
        {

            List<Phone> phoneList = new List<Phone>();
            Phone PH = null;
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetPhoneListByFilter", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            SqlDataReader dr;

            try
            {
                if (id != null)
                {
                    param = new SqlParameter("ID", id);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("ID", DBNull.Value));
              
                if (myType!= null)
                {

                    param = new SqlParameter("PrType", myType);
                    cmd.Parameters.Add(param);
                }else
                    cmd.Parameters.Add(new SqlParameter("PrType", DBNull.Value));

                if (myName != null)
                {
                    param = new SqlParameter("Name", myName);
                    cmd.Parameters.Add(param);
                }else
                    cmd.Parameters.Add(new SqlParameter("Name", DBNull.Value));

                if (MyOSTypes != null)
                {
                    param = new SqlParameter("OSs", MyOSTypes);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("OSs", DBNull.Value));

                if (MyBrands != null)
                {
                    param = new SqlParameter("Brands", MyBrands);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("Brands", DBNull.Value));

                if (myMinPrice != null)
                {
                    param = new SqlParameter("MinPrice", myMinPrice);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("MinPrice", DBNull.Value));

                if (myMaxPrice != null)
                {
                    param = new SqlParameter("MaxPrice", myMaxPrice);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("MaxPrice", DBNull.Value));

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {

               

                    PH = new Phone();
                    PH.Id = Convert.ToInt32(dr["ID"]);
                    PH.Name = dr["Name"].ToString();
                    PH.Brand = (Brands)Convert.ToByte(dr["Brand"]);
                    PH.Available = Convert.ToBoolean(dr["Available"]);
                    if (dr["PublishDate"] == DBNull.Value)
                        PH.PublishDate = null;
                    else
                        PH.PublishDate = Convert.ToDateTime(dr["PublishDate"]);

                    if (dr["ImgUrl"] == DBNull.Value)
                        PH.ImgURL = null;
                    else
                        PH.ImgURL = dr["ImgUrl"].ToString();
                    PH.PrType = (ProductType)Convert.ToByte(dr["PrType"]);
                    PH.OS = (OsType)Convert.ToByte(dr["OS"]);

                    if (dr["Price"] == DBNull.Value)
                        PH.price = null;
                    else
                        PH.price = (float)(double)(dr["Price"]);

                    if (dr["DiscountPrice"] == DBNull.Value)
                        PH.DiscountPrice = null;
                    else
                        PH.DiscountPrice = (float)(double)(dr["DiscountPrice"]);

                    PH.Summery = dr["Summery"].ToString();
                    PH.Sale= Convert.ToBoolean(dr["Sale"]);

                    phoneList.Add(PH);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return phoneList;

        }

        public static List<Phone> GetDealsList(int? id, string MyBrands, ProductType? myType, string myName, string MyOSTypes, float? myMinPrice, float? myMaxPrice, bool mySale =true )
        {

            List<Phone> phoneList = new List<Phone>();
            Phone PH = null;
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetDealsList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            SqlDataReader dr;

            try
            {
                if (id != null)
                {
                    param = new SqlParameter("ID", id);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("ID", DBNull.Value));

                if (myType != null)
                {

                    param = new SqlParameter("PrType", myType);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("PrType", DBNull.Value));

                if (myName != null)
                {
                    param = new SqlParameter("Name", myName);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("Name", DBNull.Value));

                if (MyOSTypes != null)
                {
                    param = new SqlParameter("OSs", MyOSTypes);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("OSs", DBNull.Value));

                if (MyBrands != null)
                {
                    param = new SqlParameter("Brands", MyBrands);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("Brands", DBNull.Value));

                if (myMinPrice != null)
                {
                    param = new SqlParameter("MinPrice", myMinPrice);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("MinPrice", DBNull.Value));

                if (myMaxPrice != null)
                {
                    param = new SqlParameter("MaxPrice", myMaxPrice);
                    cmd.Parameters.Add(param);
                }
                else
                    cmd.Parameters.Add(new SqlParameter("MaxPrice", DBNull.Value));


                param = new SqlParameter("Sale", mySale);
                cmd.Parameters.Add(param);
           

                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {



                    PH = new Phone();
                    PH.Id = Convert.ToInt32(dr["ID"]);
                    PH.Name = dr["Name"].ToString();
                    PH.Brand = (Brands)Convert.ToByte(dr["Brand"]);
                    PH.Available = Convert.ToBoolean(dr["Available"]);
                    if (dr["PublishDate"] == DBNull.Value)
                        PH.PublishDate = null;
                    else
                        PH.PublishDate = Convert.ToDateTime(dr["PublishDate"]);

                    if (dr["ImgUrl"] == DBNull.Value)
                        PH.ImgURL = null;
                    else
                        PH.ImgURL = dr["ImgUrl"].ToString();
                    PH.PrType = (ProductType)Convert.ToByte(dr["PrType"]);
                    PH.OS = (OsType)Convert.ToByte(dr["OS"]);

                    if (dr["Price"] == DBNull.Value)
                        PH.price = null;
                    else
                        PH.price = (float)(double)(dr["Price"]);

                    if (dr["DiscountPrice"] == DBNull.Value)
                        PH.DiscountPrice = null;
                    else
                        PH.DiscountPrice = (float)(double)(dr["DiscountPrice"]);

                    PH.Summery = dr["Summery"].ToString();
                    PH.Sale = Convert.ToBoolean(dr["Sale"]);

                    phoneList.Add(PH);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return phoneList;

        }

        public static int CreatePhone (Phone myPhone)
        {

            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("CreatePhone", con);
            cmd.CommandType = CommandType.StoredProcedure;
            int created;

            try
            {


                con.Open();

                cmd.Parameters.AddWithValue("@ID", (int)myPhone.Id);
                cmd.Parameters.AddWithValue("@Name", myPhone.Name.ToString());
                cmd.Parameters.AddWithValue("@Brand",(Brands)myPhone.Brand);
                cmd.Parameters.AddWithValue("@Available",Convert.ToBoolean(myPhone.Available));
                if (myPhone.PublishDate == null)
                    cmd.Parameters.AddWithValue("@PublishDate", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@PublishDate", Convert.ToDateTime(myPhone.PublishDate));
                cmd.Parameters.AddWithValue("@ImgURL", myPhone.ImgURL.ToString());
                cmd.Parameters.AddWithValue("@OS",(OsType)myPhone.OS);
                cmd.Parameters.AddWithValue("@PrType", (ProductType)myPhone.PrType);
                created = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return created;

        }



        public static int GetPhoneCount()
        {

            int MyPhoneCount = 0;

            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("GetPhoneCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader dr;

            
            try
            {
                con.Open();

                MyPhoneCount = (int)cmd.ExecuteScalar();

                con.Close();
              
            }
            catch (Exception)
            {
                throw;
            }
            return MyPhoneCount;
        }

    }
}