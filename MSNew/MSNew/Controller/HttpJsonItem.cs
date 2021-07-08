using System;
using System.Collections.Generic;
using System.Text;

namespace MSNew.Controller
{
    public class HttpJsonItem
    {
        public class PostDocSpecResponce
        {
            public string type { get; set; }
            public string nommodif { get; set; }
            public string newmark_sign { get; set; }
            public string pack_quant { get; set; }
            public string comment { get; set; }
            public string doc_rn { get; set; }
            public string box_rn { get; set; }
        }

        public class GetDocHeadResponce
        {
            public string TC_RN { get; set; }
            public string SLOADER { get; set; }
            public string SGOODMAN { get; set; }
        }

        public class GetDocSpecResponce
        {
            public string TC_RN { get; set; }
            public string MODIF_NAME { get; set; }
            public string NM_RN { get; set; }
            public string QUANT_NEED { get; set; }
            public string QUANT_DO { get; set; }
        }

        public class GetBoxSpecResponce
        {
            public string TC_RN { get; set; }
            public string NM_RN { get; set; }
            public string BOX_RN { get; set; }
            public string BOX_NUM { get; set; }
            public string QUANT { get; set; }
        }

        public class GetMarkSpecResponce
        {
            public string BOX_RN { get; set; }
            public string MARK_RN { get; set; }
            public string PREF { get; set; }
            public string NUMB { get; set; }
        }

        public class PostInfoResponce
        {
            public string doc_rn { get; set; }
        }

        public class GetInfoHeadResponce
        {
            public string RN { get; set; }
            public string STYPE { get; set; }
            public string SDOC_TYPE { get; set; }
            public string SDOC { get; set; }
            public string SPALLET { get; set; }
            public string SNOMMODIF { get; set; }
            public string SCOMMENT { get; set; }
        }

        public class GetInfoSpecResponce
        {
            public string RN { get; set; }
            public string PRN { get; set; }
            public string BARCODE { get; set; }
            public string SCOMMENT { get; set; }
        }

        public class PostLinkResponce
        {
            public string box_rn { get; set; }
            public string comment { get; set; }
        }

        public class GetLinkSpecResponce
        {
            public string PRN { get; set; }
            public string SHORT_BARCODE { get; set; }
            public string IS_LINKED { get; set; }
        }

        public class PostRevizorResponce
        {
            public string comment { get; set; }
            public string box_rn { get; set; }
            public string out_responce { get; set; }
            public string doc_rn { get; set; }
        }

        public class GetRevizorHeadResponce
        {
            public string RN { get; set; }
            public string DOC_NUM { get; set; }
            public string NOTE { get; set; }
            public string QUANT { get; set; }
            public string QUANT_DO { get; set; }
        }

        public class GetRevizorBoxResponce
        {
            public string RN { get; set; }
            public string DOC_RN { get; set; }
            public string BOX_NUM { get; set; }
            public string PROD_NAME { get; set; }
            public string CAPACITY { get; set; }
            public string QUANTITY { get; set; }
            public string QUANT_DO { get; set; }
            public string SCOMMENTS { get; set; }
        }

        public class GetRevizorMarkResponce
        {
            public string RN { get; set; }
            public string PRN { get; set; }
            public string PREF { get; set; }
            public string NUMB { get; set; }
            public string FULLNAME { get; set; }
        }

        public class PostDmnRevizorResponce
        {
            public string out_rn { get; set; }
            public string out_error { get; set; }
        }

        public class GetDmnRevizorHeadResponce
        {
            public string RN { get; set; }
            public string DOC { get; set; }
            public string ALL_QUANT { get; set; }
        }

        public class GetDmnRevizorSpecResponce
        {
            public string RN { get; set; }
            public string PRN { get; set; }
            public string BARCODE { get; set; }
            public string NOMEN_NAME { get; set; }
            public string QUANT { get; set; }
            public string QUANT_NEED { get; set; }
        }
    }
}
