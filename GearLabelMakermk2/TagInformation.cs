using System.Collections.Generic;
using System.Windows;

namespace GearLabelMakermk2
{
    public class TagInformation
    {
        public List<int> serialNumbers { get; set; } = new List<int>();//
        public string serialNumber { get; set; }//
        string job { get; set; }//
        int line { get; set; }//
        public string partNumber { get; set; }//
        public string CUNumber { get; set; }//
        public string customerTag { get; set; }//
        public string customerPONumber { get; set; }//
        public string discription { get; set; }//
        public int ratio { get; set; } //TODO get raito and weight
        public double weight { get; set; }

        public TagInformation(string jobnumber)
        {
            if (jobnumber.Length == 8)
            {
                serialNumber = jobnumber;
                job = serialNumber.Substring(0, 6);
                line = int.Parse( serialNumber.Substring(6));
            }
            else
            {
                serialNumber = jobnumber.Substring(0, 8);
                serialNumbers.Add(int.Parse(jobnumber.Substring(8)));
                job = serialNumber.Substring(0, 6);
                line = int.Parse( serialNumber.Substring(6));
            }
            getParameters();
            //MessageBox.Show(Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented));

        }
        private void getDescription(mTMS.ORDS_DATARow row)
        {
            mTMS.PART_DATADataTable dtPART = new mTMS.PART_DATADataTable();
            mTMSTableAdapters.PART_DATATableAdapter dtaPART = new mTMSTableAdapters.PART_DATATableAdapter();
            dtaPART.Fill(dtPART, row.ORDS1_CO_SITE, row.ORDS3_PART);
            discription = dtPART[0].PARTDESC.Trim();
        }
        private void getParameters()
        {
            mTMS.ORDS_DATADataTable dtORDS = new mTMS.ORDS_DATADataTable();
            mTMSTableAdapters.ORDS_DATATableAdapter dtaORDS = new mTMSTableAdapters.ORDS_DATATableAdapter();
            dtaORDS.Fill(dtORDS, job.PadRight(12), "0");
            customerPONumber = dtORDS[0].ORDS2_CUSSUP_REF.Trim();
            dtaORDS.Fill(dtORDS, job.PadRight(12), line.ToString());
            //Get the number of tags to print out
            mTMS.ORDS_DATARow row = (mTMS.ORDS_DATARow)dtORDS.Rows[0];
            customerTag = row.ORDSCUS_SUP_PART.Trim();
            if (dtORDS.Rows.Count > 0)
            {

                CUNumber = row.ORDS4_CUS_SUP.Trim();
                partNumber = row.ORDS3_PART.Trim();

                if (serialNumbers.Count == 0)
                {

                    for (int i = 0; i <= int.Parse(row.ORDSQTY_REQUIRED.ToString()); i++)
                    {
                        serialNumbers.Add(i);
                    }
                }
            }
            getDescription(row);
        }

    }
}
