using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using MSNew.Controller;
using MSNew.Model.Doc;
using MSNew.View.Doc;
using Plugin.Settings;
using Xamarin.Forms;
using Newtonsoft.Json;
using static MSNew.Controller.HttpJsonItem;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MSNew.ViewModel.Doc
{
    public class DocPageVM : INotifyPropertyChanged
    {
        public string term_name { get; set; }
        public string doc_rn { get; set; }
        public string box_rn { get; set; }
        public string del_sign { get; set; }
        public string quant { get; set; }
        public bool isPackquantShow;
        public string _selectNommodif { get; set; }
        public string lastBarcode { get; set; }

        public ObservableCollection<DocSpec> DocSpecs { get; set; }

        public ICommand DocSend { get; set; }
        public ICommand BoxShow { get; set; }
        public ICommand DocSpecCommentShow { get; set; }
        public ICommand DocSpecAddressShow { get; set; }
        public INavigation Navigation { get; set; }

        public DocPageVM(string _doc_rn)
        {
            DocSpecs = new ObservableCollection<DocSpec>();
            SelectDocSpec = new DocSpec();
            DocSend = new Command(SendDoc);
            BoxShow = new Command(ShowBox);
            DocSpecCommentShow = new Command(CommentShow);
            DocSpecAddressShow = new Command(AddressShow);

            term_name = CrossSettings.Current.GetValueOrDefault("TerminalNumber", "");
            doc_rn = _doc_rn;
            del_sign = "0";
            box_rn = "0";
            quant = "0";
            DocHeadNumb = "Накладная не указана";
            Address = "Адрес не указан";
            Comment = "";
            lastBarcode = "";
            isPackquantShow = false;
            _selectNommodif = "";
        }

        public void startPage()
        {

            if (doc_rn != "0")
            {
                HttpControler.SendGetDocSpec(doc_rn);
                HttpControler.SendGetDocHead(doc_rn);
                HttpControler.SendPostDocSpec(term_name, doc_rn, "0", "0", "0", "0", "PostDocSpec");
            }
            MessagingCenter.Subscribe<string, string>("MainActivity", "GetBarcode", (sender, arg) =>
            {
                work(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "PostDocSpec", (sender, arg) =>
            {
                PostDocSpecResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetDocHead", (sender, arg) =>
            {
                GetDocHeadResponce(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "GetDocSpec", (sender, arg) =>
            {
                fillList(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("DocSpec", "BoxNum", (sender, arg) =>
            {
                GetBoxNum(arg.Trim());
            });
            MessagingCenter.Subscribe<string, string>("HttpControler", "Error", (sender, arg) =>
            {
                showError(arg.Trim());
            });
        }

        public void endPage()
        {
            MessagingCenter.Unsubscribe<string, string>("MainActivity", "GetBarcode");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "PostDocSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetDocHead");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "GetDocSpec");
            MessagingCenter.Unsubscribe<string, string>("HttpControler", "Error");
            MessagingCenter.Unsubscribe<string, string>("DocSpec", "BoxNum");
        }

        public void GetDocHeadResponce(string content)
        {
            try
            {
                if (!(content == "" || content == "[]"))
                {
                    GetDocHeadResponce tmp = JsonConvert.DeserializeObject<GetDocHeadResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    DocHeadLoader = tmp.SLOADER;
                    DocHeadGoodsExpert = tmp.SGOODMAN;
                    DocHeadNumb = tmp.TC_NUMB;
                    Address = tmp.ADDRESS;
                    Comment = tmp.COMMENTS;
                }
            }
            catch (Exception ex)
            {
                DocHeadNumb = "Накладная не указана";
                Address = "Адрес не указан";
                this.showError(ex.Message);
            }
        }

        public void GetBoxNum(string box_num)
        {
            UserDialogs.Instance.ShowLoading("Обмен данными");
            HttpControler.SendPostDocSpec(term_name, lastBarcode, doc_rn, box_rn, del_sign, box_num, "PostDocSpec");
        }

        public void PostDocSpecResponce(string content)
        {
            try
            {
                PostDocSpecResponce responce = JsonConvert.DeserializeObject<PostDocSpecResponce>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                quant = "0";
                _selectNommodif = "";
                if (responce.type == "-1")  //заблокированный документ, возможен только просмотр
                {
                    DocHeadNumb = responce.comment;
                    doc_rn = responce.doc_rn;
                    del_sign = "-1";
                    UserDialogs.Instance.Alert("Документ доступен только для ПРОСМОТРА", "Внимание");
                }
                if (responce.type == "0")  //Ошибка
                {
                    this.showError(responce.comment);
                }
                if (responce.type == "12")  //Отправка документа
                {
                    UserDialogs.Instance.Alert(responce.comment, "Отправка документа", "Ок");
                    UserDialogs.Instance.HideLoading();
                }
                if (responce.type == "6")  //Грузчик
                {
                    DocHeadLoader = responce.comment;
                }
                if (responce.type == "7")  //Товаровед
                {
                    DocHeadGoodsExpert = responce.comment;
                }
                if (responce.type == "11")  //Марка
                {
                    _selectNommodif = responce.nommodif;
                    MarkDetail = responce.comment;
                }
                if (responce.type == "13") //паллет
                {
                    
                }
                if (responce.type == "10") //коробка
                {
                    _selectNommodif = responce.nommodif;
                    if (responce.box_rn != null && responce.box_rn != "")
                    {
                        box_rn = responce.box_rn;
                        lastBarcode = "";
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                        showInput("Ввод количества", "1/" + responce.pack_quant + " " + responce.comment, responce.pack_quant);
                }
                if (responce.type == "1" || responce.type == "2" || responce.type == "3" || responce.type == "4" || responce.type == "5" || responce.type == "14")
                {
                    isPackquantShow = false;
                    if (responce.type == "3")
                    {
                        isPackquantShow = true;
                    }

                    DocHeadNumb = responce.comment;
                    doc_rn = responce.doc_rn;
                    del_sign = "0";
                }
                HttpControler.SendGetDocHead(doc_rn);
                HttpControler.SendGetDocSpec(doc_rn);
            }
            catch (Exception ex)
            {
                this.showError(ex.Message);
            }
        }

        public async void showInput(string title, string message, string quant)
        {
            PromptConfig pc = new PromptConfig();
            pc.Title = title;
            pc.Message = message;
            pc.OkText = "ОК";
            if (isPackquantShow)
                pc.Text = quant;
            pc.InputType = InputType.Number;

            var prompt = await UserDialogs.Instance.PromptAsync(pc);
            if (prompt.Ok)
                MessagingCenter.Send<string, string>("DocSpec", "BoxNum", prompt.Text);
        }

        public async void SendDoc()
        {
            if (del_sign == "-1")
            {
                UserDialogs.Instance.Alert("Документ доступен только для ПРОСМОТРА", "Внимание");
            }
            else
            {
                if (TotalQuantDo == TotalQuantNeed)
                {
                    var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                    {
                        Message = "Отправка документа",
                        OkText = "Отправить",
                        CancelText = "Отмена"
                    });
                    if (result)
                    {
                        UserDialogs.Instance.ShowLoading("Обмен данными");
                        HttpControler.SendPostDocSpec(term_name, "", doc_rn, box_rn, "2", quant, "PostDocSpec");
                    }
                }
                else
                {
                    var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                    {
                        Message = "Собраны не все позиции. Документ отправится с РАСХОЖДЕНИЯМИ!!!",
                        OkText = "Отправить",
                        CancelText = "Отмена"
                    });
                    if (result)
                    {
                        UserDialogs.Instance.ShowLoading("Обмен данными");
                        HttpControler.SendPostDocSpec(term_name, "", doc_rn, box_rn, "2", quant, "PostDocSpec");
                    }
                }
            }
        }

        public void fillList(string content)
        {
            DocSpecs.Clear();
            TotalQuantDo = 0;
            TotalQuantNeed = 0;
            try
            {
                List<GetDocSpecResponce> tmp = JsonConvert.DeserializeObject<List<GetDocSpecResponce>>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                tmp.Sort(new Comparison<GetDocSpecResponce>((x, y) => {
                    if (String.Compare(x.MODIF_NAME, y.MODIF_NAME, true) != 0)
                        return String.Compare(x.MODIF_NAME, y.MODIF_NAME, true);
                    else
                        return Convert.ToInt32(!String.IsNullOrEmpty(y.QUANT_NEED) ? y.QUANT_NEED : "0").CompareTo(Convert.ToInt32(!String.IsNullOrEmpty(x.QUANT_NEED) ? x.QUANT_NEED : "0"));
                }));

                foreach (GetDocSpecResponce q in tmp)
                {
                    DocSpecs.Add(new DocSpec 
                        { 
                            Rn = q.NM_RN, Name = q.MODIF_NAME, QuantDo = q.QUANT_DO, QuantNeed = q.QUANT_NEED, Capacity = Convert.ToDouble(q.CAPACITY.Replace('.', ',')).ToString("0.###") 
                        });
                    TotalQuantDo += Convert.ToInt32(String.IsNullOrEmpty(q.QUANT_DO) ? "0" : q.QUANT_DO);
                    TotalQuantNeed += Convert.ToInt32(String.IsNullOrEmpty(q.QUANT_NEED) ? "0" : q.QUANT_NEED);
                    if (_selectNommodif != "")
                    {
                        if (q.NM_RN == _selectNommodif)
                            SelectDocSpec = new DocSpec { Rn = q.NM_RN, Name = q.MODIF_NAME, QuantDo = q.QUANT_DO, QuantNeed = q.QUANT_NEED, Capacity = q.CAPACITY };
                    }
                }
                if (_selectNommodif != "")
                    MessagingCenter.Send<string, DocSpec>("DocSpecPageXaml", "Scroll", SelectDocSpec);
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                this.showError(ex.Message);
            }
        }

        public void ShowBox()
        {
            if (SelectDocSpec != null)
            {
                Navigation.PushAsync(new BoxPage(doc_rn,SelectDocSpec.Rn,SelectDocSpec.Name,del_sign));
            }
        }

        public void AddressShow()
        {
            UserDialogs.Instance.Alert(_address, "Адрес");
        }
        public void CommentShow()
        {
            UserDialogs.Instance.Alert(_comment, "Комментарий");
        }

        public void work(string _barcode)
        {
            UserDialogs.Instance.ShowLoading("Обмен данными");
            lastBarcode = _barcode;
            HttpControler.SendPostDocSpec(term_name, _barcode, doc_rn, box_rn, del_sign, quant, "PostDocSpec");
        }

        public void showError(string error)
        {
            UserDialogs.Instance.HideLoading();
            UserDialogs.Instance.Alert(error, "Ошибка", "Ок");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string _docHeadNumb { get; set; }
        public string DocHeadNumb
        {
            get
            {
                return _docHeadNumb;
            }
            set
            {
                if (_docHeadNumb != value)
                {
                    _docHeadNumb = value;
                    OnPropertyChanged("DocHeadNumb");
                }
            }
        }

        private string _docHeadGoodsExpert { get; set; }
        public string DocHeadGoodsExpert
        {
            get
            {
                if (String.IsNullOrEmpty(_docHeadGoodsExpert) || String.IsNullOrWhiteSpace(_docHeadGoodsExpert))
                    return "Товаровед";
                else
                    return _docHeadGoodsExpert;
            }
            set
            {
                if (_docHeadGoodsExpert != value)
                {
                    _docHeadGoodsExpert = value;
                    OnPropertyChanged("DocHeadGoodsExpert");
                }
            }
        }

        private string _docHeadLoader { get; set; }
        public string DocHeadLoader
        {
            get
            {
                if (String.IsNullOrEmpty(_docHeadLoader) || String.IsNullOrWhiteSpace(_docHeadLoader))
                    return "Грузчик";
                else
                    return _docHeadLoader;
            }
            set
            {
                if (_docHeadLoader != value)
                {
                    _docHeadLoader = value;
                    OnPropertyChanged("DocHeadLoader");
                }
            }
        }

        private string _markDetail { get; set; }
        public string MarkDetail
        {
            get
            {
                return _markDetail;
            }
            set
            {
                if (_markDetail != value)
                {
                    _markDetail = value;
                    OnPropertyChanged("MarkDetail");
                }
            }
        }

        private DocSpec _selectedDocSpec { get; set; }
        public DocSpec SelectDocSpec 
        {
            get
            {
                return _selectedDocSpec;
            }
            set
            {
                if (_selectedDocSpec != value)
                {
                    if (_selectedDocSpec != null) _selectedDocSpec.IsSelected = false;

                    value.IsSelected = true;

                    _selectedDocSpec = value;
                    OnPropertyChanged("SelectDocSpec");
                }
            }
        }

        private string _comment { get; set; }
        public string Comment
        {
            get
            {
                if (_comment.Length > 20)
                    return _comment.Substring(0, 20) + "...";
                else
                    return _comment;
            }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private string _address { get; set; }
        public string Address
        {
            get
            {
                if (_address.Length > 20)
                    return _address.Substring(0, 20) + "...";
                else
                    return _address;
            }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        private int _totalQuantNeed { get; set; }
        public int TotalQuantNeed
        {
            get
            {
                return _totalQuantNeed;
            }
            set
            {
                if (_totalQuantNeed != value)
                {
                    _totalQuantNeed = value;
                    OnPropertyChanged("TotalQuantNeed");
                    OnPropertyChanged("TotalQuantDoTC");
                }
            }
        }

        public string TotalQuantDoTC
        {
            get
            {
                if (TotalQuantNeed == TotalQuantDo)
                    return "Green";
                else
                    return "Red";
            }
        }

        private int _totalQuantDo { get; set; }
        public int TotalQuantDo
        {
            get
            {
                return _totalQuantDo;
            }
            set
            {
                if (_totalQuantDo != value)
                {
                    _totalQuantDo = value;
                    OnPropertyChanged("TotalQuantDo");
                    OnPropertyChanged("TotalQuantDoTC");
                }
            }
        }

    }
}
