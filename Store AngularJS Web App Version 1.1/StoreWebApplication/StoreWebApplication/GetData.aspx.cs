using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeWebApliccatin
{
    public partial class GetData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // q=1 -->get our team list
            if (Request.Params["q"] != null && Request.Params["q"] == "1")
            {



                //do stuff
                List<Employee> empLst = Employee.GetEmployeeList();
                

                //string json = "{\"name\":\"Joe\"}";
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myEmpLst = javaScriptSerializer.Serialize(empLst);
                string json = empLst.ToString();
                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myEmpLst);
                Response.End();

            }
            //q=2 -->get phone list
            else if (Request.Params["q"] != null && Request.Params["q"] == "2")
            {

                ProductType? PType = null;
                //do stuff
                if (Request.Params["TP"]!= "null")
                PType = (ProductType)Convert.ToByte(Request.Params["TP"]);

                List<Phone> phList = Phone.GetPhonesListByType(PType);

                string myJson = "{\"OSs\":[{\"n\":\""+ OsType.Android.ToString()+ "\", \"val\": \""+Convert.ToInt32 (OsType.Android) + "\"}, {\"n\":\""+ OsType.IOS.ToString() + "\", \"val\":\""+ Convert.ToInt32(OsType.IOS) + "\"}], \"Brds\":[{\"n\":\""+ Brands.Samsung.ToString() + "\" , \"val\":\""+ Convert.ToInt32(Brands.Samsung) + "\"},{\"n\":\""+ Brands.Apple.ToString() + "\" , \"val\":\""+ Convert.ToInt32(Brands.Apple) + "\"},{\"n\":\""+ Brands.Microsoft.ToString() + "\" , \"val\":\""+ Convert.ToInt32(Brands.Microsoft) + "\"}, {\"n\":\""+ Brands.Sony.ToString() + "\" , \"val\":\""+ Convert.ToInt32(Brands.Sony) + "\"}] ,\"DataArr\":[";
                int i = 1;
                foreach (Phone myPh in phList) {
                    myJson += "{\"Id\":" + myPh.Id.ToString() + ",\"Name\":" + "\"" + myPh.Name.ToString() + "\"" + ",\"Price\":" + "\"" + myPh.Price.ToString() + "\""+ ",\"DPrice\":" + "\"" + myPh.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                    #region Brand Name SC
                    switch (myPh.Brand)
                    {
                        case Brands.Apple:

                            myJson += "\"Apple\"";
                            break;
                        case Brands.Microsoft:
                            myJson += "\"Microsoft\"";
                            break;
                        case Brands.Samsung:
                            myJson += "\"Samsung\"";
                            break;
                        case Brands.Sony:
                            myJson += "\"Sony\"";
                            break;
                        case Brands.HTC:
                            myJson += "\"HTC\"";
                            break;
                    }
                    #endregion

                    myJson += ",\"PublishDate\":" + (myPh.PublishDate == null ? "null" : "\"" + myPh.PublishDate.ToString() + "\"");
                    myJson += ",\"ImgUrl\":" + (myPh.ImgURL == null ? "null" : "\"" + myPh.ImgURL.ToString() + "\"");
                    myJson += ",\"PrType\":";
                    #region Product Type SC
                    switch (myPh.PrType)
                    {
                        case ProductType.Phone:
                            myJson += "\"phone\"";
                            break;
                        case ProductType.Tablet:
                            myJson += "\"tablet\"";
                            break;

                    }
                    #endregion

                    myJson += ",\"Available\":" + "\""+ myPh.Available + "\"";
                    myJson += ",\"Sale\":" + "\"" + myPh.Sale + "\"";
                    myJson += ",\"Summery\":" + "\"" + myPh.Summery.ToString() + "\"";
                    myJson += ",\"OS\":";
                    #region Operating Sysyem
                    switch (myPh.OS)
                    {
                        case OsType.Android:
                            myJson += "\"Android\"";
                            break;
                        case OsType.IOS:
                            myJson += "\"IOS\"";
                            break;

                    }
                    #endregion
                    if (i == phList.Count())
                        myJson += "}";
                    else {
                        myJson += "},";
                        i++;
                    }

                }               
                myJson += "]}";
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myPhoneList = javaScriptSerializer.Serialize(myJson);

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myJson);
                Response.End();

            }
            // q = 1-- > get phone deals
            else if(Request.Params["q"] != null && Request.Params["q"] == "3")
            {


                if (Request.Params["JD"] != null)

                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic myObj = serializer.Deserialize<object>(Request.Params["JD"].ToString());
                    string PhoneName = myObj["n"];
                    string myOSTypes = "";
                    string myBrands = "";
                    float? myMinPrice = null;
                    float? myMaxPrice = null;
                    ProductType? PType = null;
                    //do stuff
                    if (myObj["TP"] != null)
                        PType = (ProductType)Convert.ToByte(myObj["TP"]);

                    if (myObj["OS"] != null)
                        myOSTypes = myObj["OS"].ToString().Replace("[", "").Replace("]", "");

                    if (myObj["Br"] != null)
                        myBrands = myObj["Br"].ToString().Replace("[", "").Replace("]", "");

                    if (myObj["minP"] != null)
                        myMinPrice = (float)Convert.ToInt32(myObj["minP"]);

                    if (myObj["maxP"] != null)
                        myMaxPrice = (float)Convert.ToInt32(myObj["maxP"]);

                    List<Phone> phoneFilteredList = Phone.GetDealsList(null, myBrands, PType, PhoneName, myOSTypes, myMinPrice, myMaxPrice,true);

                    string myJson ="";
                    myJson += "{\"DT\":[{\"n\": \""+ ProductType.Phone.ToString() +"\",  \"val\":\""+Convert.ToInt32(ProductType.Phone) +"\"},{\"n\": \""+ ProductType.Tablet.ToString() + "\",  \"val\":\""+Convert.ToInt32(ProductType.Tablet) + "\"}],\"OSs\":[{\"n\":\"" + OsType.Android.ToString() + "\", \"val\": \"" + Convert.ToInt32(OsType.Android) + "\"}, {\"n\":\"" + OsType.IOS.ToString() + "\", \"val\":\"" + Convert.ToInt32(OsType.IOS) + "\"}], \"Brds\":[{\"n\":\"" + Brands.Samsung.ToString() + "\" , \"val\":\"" + Convert.ToInt32(Brands.Samsung) + "\"},{\"n\":\"" + Brands.Apple.ToString() + "\" , \"val\":\"" + Convert.ToInt32(Brands.Apple) + "\"},{\"n\":\"" + Brands.Microsoft.ToString() + "\" , \"val\":\"" + Convert.ToInt32(Brands.Microsoft) + "\"}, {\"n\":\"" + Brands.Sony.ToString() + "\" , \"val\":\"" + Convert.ToInt32(Brands.Sony) + "\"}], ";
                    myJson += "\"DataArr\":[";
                    int i = 1;
                    foreach (Phone myPh in phoneFilteredList)
                    {
                        myJson += "{\"Id\":" + myPh.Id.ToString() + ",\"Name\":" + "\"" + myPh.Name.ToString() + "\"" + ",\"Price\":" + "\"" + myPh.Price.ToString() + "\"" + ",\"DPrice\":" + "\"" + myPh.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                        #region Brand Name SC
                        switch (myPh.Brand)
                        {
                            case Brands.Apple:

                                myJson += "\"Apple\"";
                                break;
                            case Brands.Microsoft:
                                myJson += "\"Microsoft\"";
                                break;
                            case Brands.Samsung:
                                myJson += "\"Samsung\"";
                                break;
                            case Brands.Sony:
                                myJson += "\"Sony\"";
                                break;
                            case Brands.HTC:
                                myJson += "\"HTC\"";
                                break;
                        }
                        #endregion
                        myJson += ",\"PublishDate\":" + (myPh.PublishDate == null ? "null" : "\"" + myPh.PublishDate.ToString() + "\"");
                        myJson += ",\"ImgUrl\":" + (myPh.ImgURL == null ? "null" : "\"" + myPh.ImgURL.ToString() + "\"");
                        myJson += ",\"Sale\":" + "\"" + myPh.Sale + "\"";
                        myJson += ",\"Summery\":" + "\"" + myPh.Summery.ToString() + "\"";
                        myJson += ",\"PrType\":";

                        #region Product Type SC
                        switch (myPh.PrType)
                        {
                            case ProductType.Phone:
                                myJson += "\"phone\"";
                                break;
                            case ProductType.Tablet:
                                myJson += "\"tablet\"";
                                break;

                        }
                        #endregion
                        if (i == phoneFilteredList.Count())
                            myJson += "}";
                        else
                        {
                            myJson += "},";
                            i++;
                        }

                    }
                    myJson += "]}";
                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string myPhoneList = javaScriptSerializer.Serialize(myJson);

                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(myJson);
                    Response.End();

                }

            }
            // q = 4-- > get phone list by Filter
            else if(Request.Params["q"] != null && Request.Params["q"] == "4")
            {

                if (Request.Params["JD"] != null)

                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic myObj = serializer.Deserialize<object>(Request.Params["JD"].ToString());
                    string PhoneName = myObj["n"];
                    string myOSTypes = "";
                    string myBrands="";
                    float? myMinPrice = null;
                    float? myMaxPrice = null;
                    ProductType PType= (ProductType)Convert.ToByte(myObj["TP"]);
                    
                  if (myObj["OS"] != null) 
                    myOSTypes = myObj["OS"].ToString().Replace("[", "").Replace("]", "");

                  if (myObj["Br"] != null)
                        myBrands = myObj["Br"].ToString().Replace("[", "").Replace("]", "");

                    if (myObj["minP"] != null)
                        myMinPrice = (float)Convert.ToInt32(myObj["minP"]);

                    if (myObj["maxP"] != null)
                        myMaxPrice = (float)Convert.ToInt32(myObj["maxP"]);

                    List<Phone> phoneFilteredList = Phone.GetPhoneListByFilter(null, myBrands, PType, PhoneName, myOSTypes, myMinPrice, myMaxPrice);


                      string myJson = "[";
                    int i = 1;
                    foreach (Phone myPh in phoneFilteredList)
                    {
                        myJson += "{\"Id\":" + myPh.Id.ToString() + ",\"Name\":" + "\"" + myPh.Name.ToString() + "\""+ ",\"Price\":" + "\"" + myPh.Price.ToString() + "\"" + ",\"DPrice\":" + "\"" + myPh.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                        #region Brand Name SC
                        switch (myPh.Brand)
                        {
                            case Brands.Apple:

                                myJson += "\"Apple\"";
                                break;
                            case Brands.Microsoft:
                                myJson += "\"Microsoft\"";
                                break;
                            case Brands.Samsung:
                                myJson += "\"Samsung\"";
                                break;
                            case Brands.Sony:
                                myJson += "\"Sony\"";
                                break;
                            case Brands.HTC:
                                myJson += "\"HTC\"";
                                break;
                        }
                        #endregion
                        myJson += ",\"PublishDate\":" + (myPh.PublishDate == null ? "null" : "\"" + myPh.PublishDate.ToString() + "\"");
                        myJson += ",\"ImgUrl\":" + (myPh.ImgURL == null ? "null" : "\"" + myPh.ImgURL.ToString() + "\"");
                        myJson += ",\"Sale\":" + "\"" + myPh.Sale + "\"";
                        myJson += ",\"Summery\":" + "\"" + myPh.Summery.ToString() + "\"";
                        myJson += ",\"PrType\":";

                        #region Product Type SC
                        switch (myPh.PrType)
                        {
                            case ProductType.Phone:
                                myJson += "\"Phone\"";
                                break;
                            case ProductType.Tablet:
                                myJson += "\"Tablet\"";
                                break;

                        }
                        #endregion
                        if (i == phoneFilteredList.Count())
                            myJson += "}";
                        else
                        {
                            myJson += "},";
                            i++;
                        }

                    }
                    myJson += "]";
                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string myPhoneList = javaScriptSerializer.Serialize(myJson);

                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(myJson);
                    Response.End();

                }


           
            }
            // q = 5-- > get phone Item by ID
            else if (Request.Params["q"] != null && Request.Params["q"] == "5")
            {

                //do stuff
                int phoneID = Convert.ToInt32(Request.Params["id"]);
                Phone PhoneItem = Phone.GetPhoneByID(phoneID);

                string myJson = "";

           
                    myJson += "{\"Id\":" + PhoneItem.Id.ToString() + ",\"Name\":" + "\"" + PhoneItem.Name.ToString() + "\""+ ",\"Price\":" + "\"" + PhoneItem.Price.ToString() + "\"" + ",\"DPrice\":" + "\"" + PhoneItem.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                    #region Brand Name SC
                    switch (PhoneItem.Brand)
                    {
                        case Brands.Apple:

                            myJson += "\"Apple\"";
                            break;
                        case Brands.Microsoft:
                            myJson += "\"Microsoft\"";
                            break;
                        case Brands.Samsung:
                            myJson += "\"Samsung\"";
                            break;
                        case Brands.Sony:
                            myJson += "\"Sony\"";
                            break;
                        case Brands.HTC:
                            myJson += "\"HTC\"";
                            break;
                    }
                    #endregion
                    myJson += ",\"PublishDate\":" + (PhoneItem.PublishDate == null ? "null" : "\"" + PhoneItem.PublishDate + "\"");
                    myJson += ",\"PrType\":";
                    #region Product Type SC
                    switch (PhoneItem.PrType)
                    {
                        case ProductType.Phone:
                            myJson += "\"Phone\"";
                            break;
                        case ProductType.Tablet:
                            myJson += "\"Tablet\"";
                            break;

                    }


                #endregion

                myJson += ",\"Available\":" + "\"" + PhoneItem.Available + "\"";
                myJson += ",\"Sale\":" + "\"" + PhoneItem.Sale + "\"";
                myJson += ",\"Summery\":" + "\"" + PhoneItem.Summery.ToString() + "\"";
                myJson += ",\"Des\":" + "\"" + PhoneItem.Description.ToString() + "\"";
                myJson += ",\"OS\":";
                #region Operating Sysyem
                switch (PhoneItem.OS)
                {
                    case OsType.Android:
                        myJson += "\"Android\"";
                        break;
                    case OsType.IOS:
                        myJson += "\"IOS\"";
                        break;

                }
                #endregion

                if (PhoneItem.PrType == ProductType.Phone)
                    myJson += ",\"Img1\":\" /Images/black/phone 1-1-289.jpg\", \"Img2\":\" /Images/black/phone 1-2-289.jpg\", \"Img3\":\" /Images/black/phone 1-3-289.jpg\"";

                else if (PhoneItem.PrType == ProductType.Tablet)
                    myJson += ",\"Img1\":\" /Images/tblack/tablet 1-1-289.jpg\", \"Img2\":\" /Images/tblack/tablet 1-2-289.jpg\", \"Img3\":\" /Images/tblack/tablet 1-3-289.jpg\"";
                myJson += "}";
       

                
    
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myPhoneItem = javaScriptSerializer.Serialize(myJson);

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myJson);
                Response.End();

            }
            //q=2 -->get Home Page
            else if (Request.Params["q"] != null && Request.Params["q"] == "6")
            {

                ProductType? PType = null;
                //do stuff
                if (Request.Params["TP"] != "null")
                    PType = (ProductType)Convert.ToByte(Request.Params["TP"]);
                //get Carousel items
                List<Phone> phList = Phone.GetPhonesListByType(PType);
                //get top devices counts
                List<Phone> DevicesCnt = Phone.GetDevicesCount();
                string myJson = "{\"Devs\":{\"accessories\": 0 ,";
                int j = 1;
                foreach (Phone Dev in DevicesCnt)
                {

                    switch (Dev.PrType)
                    {
                        case ProductType.Phone:
                            myJson += "\"phone\":";
                            break;
                        case ProductType.Tablet:
                            myJson += "\"tablet\":";
                            break;

                    }
                    myJson +=  " \"" + Convert.ToInt32(Dev.MyCount) + "\"";

                    if (j == DevicesCnt.Count())
                        myJson += "";
                    else
                    {
                        myJson += ",";
                        j++;
                    }
                }
                myJson += "},";
                myJson += "\"DataArr\":[";
                int i = 1;
                foreach (Phone myPh in phList)
                {
                    myJson += "{\"Id\":" + myPh.Id.ToString() + ",\"Name\":" + "\"" + myPh.Name.ToString() + "\"" + ",\"Price\":" + "\"" + myPh.Price.ToString() + "\"" + ",\"DPrice\":" + "\"" + myPh.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                    #region Brand Name SC
                    switch (myPh.Brand)
                    {
                        case Brands.Apple:

                            myJson += "\"Apple\"";
                            break;
                        case Brands.Microsoft:
                            myJson += "\"Microsoft\"";
                            break;
                        case Brands.Samsung:
                            myJson += "\"Samsung\"";
                            break;
                        case Brands.Sony:
                            myJson += "\"Sony\"";
                            break;
                        case Brands.HTC:
                            myJson += "\"HTC\"";
                            break;
                    }
                    #endregion

                    myJson += ",\"PublishDate\":" + (myPh.PublishDate == null ? "null" : "\"" + myPh.PublishDate.ToString() + "\"");
                    myJson += ",\"ImgUrl\":" + (myPh.ImgURL == null ? "null" : "\"" + myPh.ImgURL.ToString() + "\"");
                    myJson += ",\"PrType\":";
                    #region Product Type SC
                    switch (myPh.PrType)
                    {
                        case ProductType.Phone:
                            myJson += "\"phone\"";
                            break;
                        case ProductType.Tablet:
                            myJson += "\"tablet\"";
                            break;

                    }
                    #endregion

                    myJson += ",\"Available\":" + "\"" + myPh.Available + "\"";
                    myJson += ",\"Sale\":" + "\"" + myPh.Sale + "\"";
                    myJson += ",\"Summery\":" + "\"" + myPh.Summery.ToString() + "\"";
                    myJson += ",\"OS\":";
                    #region Operating Sysyem
                    switch (myPh.OS)
                    {
                        case OsType.Android:
                            myJson += "\"Android\"";
                            break;
                        case OsType.IOS:
                            myJson += "\"IOS\"";
                            break;

                    }
                    #endregion
                    if (i == phList.Count())
                        myJson += "}";
                    else
                    {
                        myJson += "},";
                        i++;
                    }

                }
                myJson += "]";
                myJson += ",\"topSl\": [{\"t\":\"Save $144!\", \"d\":\"See how to save $144 on the Samsung Galaxy A6 when you add a line.\"},{\"t\":\"More Than Your Expect!\", \"d\":\"FREE TWO-DAY Shipping on Phones and Devices With All New Activations. USE PROMO CODE: 2DAY\"}, {\"t\":\"Check Out top-selling Accessories!\", \"d\":\"Buy over $20 of accessories, get FREE SHIPPING at checkout.\"}]";
                myJson += "}";
               var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myPhoneList = javaScriptSerializer.Serialize(myJson);

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myJson);
                Response.End();

            }
            //q=7 -->change Item featurs
            else if (Request.Params["q"] != null && Request.Params["q"] == "7")
            {

                //do stuff
                int phoneID = Convert.ToInt32(Request.Params["PId"]);




                string myJson = "{";
                if (Request.Params["F"].ToString() == "color")
                {
                    if ( Request.Params["T"].ToString() == "1") { 
                    if (Request.Params["FId"].ToString() == "pink")
                        myJson += "\"Img1\":\" /Images/pink/phone 1-1-289.jpg\", \"Img2\":\" /Images/pink/phone 1-2-289.jpg\", \"Img3\":\" /Images/pink/phone 1-3-289.jpg\"";

                    else if (Request.Params["FId"].ToString() == "gold")
                        myJson += "\"Img1\":\" /Images/gold/phone 1-1-289.jpg\", \"Img2\":\" /Images/gold/phone 1-2-289.jpg\", \"Img3\":\" /Images/gold/phone 1-3-289.jpg\"";

                    else if (Request.Params["FId"].ToString() == "black")
                        myJson += "\"Img1\":\" /Images/black/phone 1-1-289.jpg\", \"Img2\":\" /Images/black/phone 1-2-289.jpg\", \"Img3\":\" /Images/black/phone 1-3-289.jpg\"";
                    }
                    else
                    {

                        if (Request.Params["FId"].ToString() == "pink")
                            myJson += "\"Img1\":\" /Images/tpink/tablet 1-1-289.jpg\", \"Img2\":\" /Images/tpink/tablet 1-2-289.jpg\", \"Img3\":\" /Images/tpink/tablet 1-3-289.jpg\"";

                        else if (Request.Params["FId"].ToString() == "gold")
                            myJson += "\"Img1\":\" /Images/tgold/tablet 1-1-289.jpg\", \"Img2\":\" /Images/tgold/tablet 1-2-289.jpg\", \"Img3\":\" /Images/tgold/tablet 1-3-289.jpg\"";

                        else if (Request.Params["FId"].ToString() == "black")
                            myJson += "\"Img1\":\" /Images/tblack/tablet 1-1-289.jpg\", \"Img2\":\" /Images/tblack/tablet 1-2-289.jpg\", \"Img3\":\" /Images/tblack/tablet 1-3-289.jpg\"";
                    }
                }
                else if(Request.Params["F"].ToString() == "memo")
                {
                    myJson += "\"Price\":\"900.00\",\"Sale\":\"False\"";

                }
                myJson += "}";




                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myPhoneItem = javaScriptSerializer.Serialize(myJson);

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myJson);
                Response.End();

            }
        }

    }
}



  

