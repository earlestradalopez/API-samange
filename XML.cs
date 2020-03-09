using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace API_samange
{
    class XML
    {        
        public void SRL(string FILE)
        {
            StreamWriter sw = new StreamWriter(string.Format("\\\\TLXSQL-DEV-01\\F$\\ETLS\\LogicMatter Flat Files\\SOLARWINDSHELPDESK\\incidents_{0:yyyy-MM-dd}.csv", DateTime.Now));
            //StreamWriter sw = new StreamWriter(string.Format("Log\\incidents_{0:yyyy-MM-dd}.csv", DateTime.Now));
            Console.WriteLine(string.Format("Fetching data -> incidents_{0:yyyy-MM-dd}.csv", DateTime.Now));
            //sw.WriteLine("id,Number,State,Title,Priority,Category,Subcategory,Description,Assigned To,Requester,Created At,Created At (Timestamp),Site,Room #,Resolution");
            sw.Write("id,Number,State,Title,Priority,Category,Subcategory,Description,Assigned_To,Assigned_Name,Assigned_Email,Requester,Created_by,Created At,Updated_At,Tags,Site,Department,Room,Printer_Issue,Phone_Issue,IT_Price_List,Student_Name,Grade_Level,Teacher_Advisor,Parent_Contact_Information,Referred_by,Abscences_Tardy,Academic_Concerns,Alleged_Bullying,Always_Tired,Anger_Aggression_Concerns,Appears_Anxious_Worried,Behavior_Concerns_Changes,Defiant,Destructive,");
            sw.Write("Dishonesty,Easily_Distracted,Eating_Concerns,Emotional_Regulation,Family_Concerns_Changes,Fearful,Hurts_Self,Impulsive_Behaviors,Lack_of_Academic_Motivation,Loss_of_a_Loved_One_Grief,Peer_Mediation_Dispute_Resolution,Peer_Relationships_Friendships,Personal_Hygiene,Personal_Identity,Poor_Self_Image_Lacks_Confidence,Sadness,Social_Skills,Substance_Abuse,Thoughts_of_Suicide,Withdrawn,CPS_Report,Clarify_your_concerns,Have_you_informed_parent_guardian_about_your_concern,Date_Informed,SPED,504,Outside_Counseling,Unknown_none,");
            sw.WriteLine("Resolution_Code,Resolution,CC,Resolved_At,Closed_At,Resolved_By,Resolved_By_Name");
            Hashtable HT = new Hashtable();
            XmlSerializer serializer = new XmlSerializer(typeof(SER.incidents));
            using (FileStream fileStream = new FileStream("XML\\TOTAL.xml", FileMode.Open))
            {
                SER.incidents result = (SER.incidents)serializer.Deserialize(fileStream);

                for (int x = 0; x < result.INC.Count; x++)
                {                      

                    string[] CUSTOMS_NUM = new string[45];
                    for (int z = 0; z < CUSTOMS_NUM.Length; z++)
                        CUSTOMS_NUM[z] = string.Empty;
                    if (result.INC[x].custom_fields_values.Count != 0)
                    {
                        if (result.INC[x].custom_fields_values[0].CFV.Count != 0)
                        {
                            int y = 0;
                            string[] CUSTOMS = { "Printer Issue", "Phone Issue", "Room #", "Student Name", "Grade Level", "Referred by", "Abscences/Tardy", "Academic Concerns", "Alleged Bullying", "Always Tired", "Anger/Aggression Concerns", "Appears Anxious/ Worried", "Behavior Concerns/Changes", "Defiant", "Destructive" };
                            foreach (string CSM in CUSTOMS)
                            {
                                if (result.INC[x].custom_fields_values[0].CFV[0].name.Equals(CSM))
                                {
                                    CUSTOMS_NUM[y] = result.INC[x].custom_fields_values[0].CFV[0].value;
                                    if (CSM.Equals("Student Name"))
                                    {
                                        for (int z = 1; z < 42; z++)
                                        {
                                            try
                                            {
                                                CUSTOMS_NUM[y + z] = result.INC[x].custom_fields_values[0].CFV[z].value.Replace("\"", "'");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                                y++;
                            }
                        }
                    }

                    /*sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"id: \"{5}\"~name: \"{6}\"~\"{7}\"~title: \"{8}\"~last_login: \"{9}\"~role: \"{10}\"~site: \"{11}\"~department: \"{12}\"~phone: \"{13}\"~mobile_phone: \"{14}\"~language: \"{15}\"~disabled: {16}\",\"{17}\",\"{18}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\"",
                                            result.INC[x].id, result.INC[x].number, result.INC[x].state, result.INC[x].CAT[0].name, result.INC[x].SUBCAT[0].name,
                                            result.INC[x].ASSIGN[0].id, result.INC[x].ASSIGN[0].name, result.INC[x].ASSIGN[0].email, NORMALIZE(result.INC[x].ASSIGN[0].title), NORMALIZE(result.INC[x].ASSIGN[0].last_login).Replace("T", " "), NORMALIZE(result.INC[x].ASSIGN[0].ROLE[0].name), NORMALIZE(result.INC[x].ASSIGN[0].SITE[0].name), NORMALIZE(result.INC[x].ASSIGN[0].DEPARTMENT[0].name), NORMALIZE(result.INC[x].ASSIGN[0].phone), NORMALIZE(result.INC[x].ASSIGN[0].mobile_phone), NORMALIZE(result.INC[x].ASSIGN[0].SITE[0].language), NORMALIZE(result.INC[x].ASSIGN[0].disabled),
                                            result.INC[x].REQUESTER[0].name, result.INC[x].created_at.Replace("T", " "), result.INC[x].SITE, ROOM, result.INC[x].updated_at.Replace("T", " ")));
                    */

                    sw.Write(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",",
                                            result.INC[x].id, result.INC[x].number, result.INC[x].state, result.INC[x].name, result.INC[x].priority, result.INC[x].CAT[0].name, result.INC[x].SUBCAT[0].name,
                                            result.INC[x].description.Replace("\"", "'"), result.INC[x].ASSIGN[0].id, result.INC[x].ASSIGN[0].name, result.INC[x].ASSIGN[0].email, result.INC[x].REQUESTER[0].name, 
                                            result.INC[x].CREATEDBY[0].name, result.INC[x].created_at.Replace("T", " "), result.INC[x].updated_at.Replace("T", " "), result.INC[x].CAT[0].default_tags,
                                            result.INC[x].SITE, result.INC[x].DEPARTMENT[0].name, CUSTOMS_NUM[2], CUSTOMS_NUM[0], CUSTOMS_NUM[1],
                                            ""//, CUSTOMS_NUM[3], CUSTOMS_NUM[4], "", ""
                                            //CUSTOMS_NUM[5], CUSTOMS_NUM[6], CUSTOMS_NUM[7], CUSTOMS_NUM[8], CUSTOMS_NUM[9]
                                            ));
                    
                    for(int z = 3; z < 45; z++)
                        sw.Write(string.Format("\"{0}\",", CUSTOMS_NUM[z]));

                    string RESOLVE_AT = string.Empty, RESOLVED_BY = string.Empty, Closed_At = string.Empty, Resolved_By_Name = string.Empty;
                    if(result.INC[x].state.Equals("Resolved"))
                    {
                        RESOLVE_AT = result.INC[x].updated_at.Replace("T", " ");
                        RESOLVED_BY = result.INC[x].ASSIGN[0].name;
                        Resolved_By_Name = result.INC[x].ASSIGN[0].name;
                    }
                    else if(result.INC[x].state.Equals("Closed"))
                        Closed_At = result.INC[x].updated_at.Replace("T", " ");
                    sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"", result.INC[x].resolution_code, result.INC[x].updated_at.Replace("T", " "), result.INC[x].CC[0].cc, RESOLVE_AT, Closed_At, RESOLVED_BY, Resolved_By_Name));
                }                                                                                                                                                                                     

            }
            sw.Close();
        }
        private string NORMALIZE(string ss)
        {            
            if(ss == null)
                return string.Empty;
            return ss;
        }
    }
    public class SER
    {
        [XmlRoot("incidents")]
        public class incidents
        {
            [XmlElement("incident")]
            public List<incident> INC { get; set; }
        }
        public class incident
        {
            [XmlElement("id")]
            public string id { get; set; }
            [XmlElement("number")]
            public string number { get; set; }
            [XmlElement("name")] // TITLE
            public string name { get; set; }
            [XmlElement("state")]
            public string state { get; set; }
            [XmlElement("priority")]
            public string priority { get; set; }
            [XmlElement("category")]
            public List<category> CAT { get; set; }
            [XmlElement("subcategory")]
            public List<subcategory> SUBCAT { get; set; }
            [XmlElement("description")]
            public string description { get; set; }
            [XmlElement("assignee")]
            public List<assignee> ASSIGN { get; set; }
            [XmlElement("requester")]
            public List<requester> REQUESTER { get; set; }
            [XmlElement("created_by")]
            public List<created_by> CREATEDBY { get; set; }
            [XmlElement("due_at")]
            public string due_at { get; set; }
            [XmlElement("created_at")]
            public string created_at { get; set; }
            [XmlElement("updated_at")]
            public string updated_at { get; set; }
            [XmlElement("SITE")]
            public string SITE { get; set; }
            [XmlElement("custom_fields_values")]
            public List<custom_fields_values> custom_fields_values { get; set; }
            [XmlElement("department")]
            public List<department> DEPARTMENT { get; set; }
            [XmlElement("resolution_code")]
            public string resolution_code { get; set; }
            [XmlElement("cc")]
            public List<_ccs_> CC { get; set; }

        }
        public class category
        {
            [XmlElement("name")] // Category
            public string name { get; set; }
            [XmlElement("default_tags")]
            public string default_tags { get; set; }
        }
        public class subcategory
        {
            [XmlElement("name")] // Subcategory
            public string name { get; set; }            
        }
        public class assignee
        {
            [XmlElement("id")] // assignee
            public string id { get; set; }
            [XmlElement("name")]
            public string name { get; set; }
            [XmlElement("email")]
            public string email { get; set; }
            [XmlElement("title")]
            public string title { get; set; }
            [XmlElement("last_login")]
            public string last_login { get; set; }
            [XmlElement("role")]
            public List<role> ROLE { get; set; }
            [XmlElement("site")]
            public List<site> SITE { get; set; }
            [XmlElement("department")]
            public List<department> DEPARTMENT { get; set; }            
            [XmlElement("phone")]
            public string phone { get; set; }
            [XmlElement("mobile_phone")]
            public string mobile_phone { get; set; }            
            [XmlElement("disabled")]
            public string disabled { get; set; }
        }
        public class role
        {
            [XmlElement("name")]
            public string name { get; set; }
        }
        public class site
        {
            [XmlElement("name")]
            public string name { get; set; }
            [XmlElement("language")]
            public string language { get; set; }
        }
        public class department
        {
            [XmlElement("name")]
            public string name { get; set; }
        }
        public class requester
        {
            [XmlElement("name")]
            public string name { get; set; }
        }
        public class created_by
        {
            [XmlElement("name")]
            public string name { get; set; }
        }
        public class custom_fields_values
        {
            [XmlElement("custom_fields_value")]
            public List<custom_fields_value> CFV { get; set; }
        }
        public class custom_fields_value
        {
            [XmlElement("value")]
            public string value { get; set; }
            [XmlElement("name")] 
            public string name { get; set; }
        }
        public class _ccs_
        {
            [XmlElement("cc")] //Room #
            public string cc { get; set; }            
        }
        
    }
    
}
