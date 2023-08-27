using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace MHN.Controllers
{
    public class GetDataPaypal
    {      
            public string GetPayPalResponse(string tx)
            {
                try
                {
                    string authToken = WebConfigurationManager.AppSettings["Token"];
                    string txToken = tx;
                    string query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
                    string url = WebConfigurationManager.AppSettings["PostUrl"];
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = "POST";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.ContentLength = query.Length;
                    StreamWriter outStreamWriter = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
                    outStreamWriter.Write(query);
                    outStreamWriter.Close();
                    StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream());
                    string strRespone = reader.ReadToEnd();
                    reader.Close();
                    if (strRespone.StartsWith("SUCCESS"))
                        return strRespone;
                    else
                        return String.Empty;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

            public Ords InformationOrder(string postData)
            {
                String sKey, sValue;
                var order = new Ords();
                try
                {
                    String[] StringArray = postData.Split('\n');
                    int i;
                    for (i = 1; i < StringArray.Length - 1; i++)
                    {
                        String[] StringArray1 = StringArray[i].Split('=');

                        sKey = StringArray1[0];
                        sValue = HttpUtility.UrlDecode(StringArray1[1]);
                        switch (sKey)
                        {
                            case "mc_gross":
                                order.GrossTotal = Convert.ToDouble(sValue);
                                break;

                            case "invoice":
                                order.InvoiceNumber = Convert.ToInt32(sValue);
                                break;

                            case "payment_status":
                                order.PaymentStatus = Convert.ToString(sValue);
                                break;

                            case "first_name":
                                order.PayerFirstName = Convert.ToString(sValue);
                                break;

                            case "mc_fee":
                                order.PaymentFee = Convert.ToDouble(sValue);
                                break;

                            case "business":
                                order.BusinessEmail = Convert.ToString(sValue);
                                break;

                            case "payer_email":
                                order.PayerEmail = Convert.ToString(sValue);
                                break;

                            case "Tx Token":
                                order.TxToken = Convert.ToString(sValue);
                                break;

                            case "last_name":
                                order.PayerLastName = Convert.ToString(sValue);
                                break;

                            case "receiver_email":
                                order.ReceiverEmail = Convert.ToString(sValue);
                                break;

                            case "item_name":
                                order.ItemName = Convert.ToString(sValue);
                                break;

                            case "mc_currency":
                                order.Currency = Convert.ToString(sValue);
                                break;

                            case "txn_id":
                                order.TransactionId = Convert.ToString(sValue);
                                break;

                            case "custom":
                                order.Custom = Convert.ToString(sValue);
                                break;

                            case "subscr_id":
                                order.SubscriberId = Convert.ToString(sValue);
                                break;
                        }
                    }

                    return order;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
       }
    
}