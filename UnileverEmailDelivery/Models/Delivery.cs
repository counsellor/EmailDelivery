using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnileverEmailDelivery.Models
{
    public class Delivery
    {
        private int _id;
        private string _brand;
        private string _cause;
        private string _email;
        private int? _concretization;

        private List<string> _colModel;

        public List<string> ColModel
        {
            get { return _colModel; }
            set { _colModel = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Cause
        {
            get { return _cause; }
            set { _cause = value; }
        }

        public int? Concretization
        {
            get { return _concretization; }
            set { _concretization = value; }
        }

        public Delivery(int _id, string _brand, string _cause, string _email, string _concretization, string _color)
        {
            ColModelFill();

            Id = _id;
            Brand = _brand;
            Cause = _cause;
            Email = _email;

            var parseResult = 0;
            if (Int32.TryParse(_concretization, out parseResult))
            {
                Concretization = parseResult;
            }
            else
            {
                Concretization = null;
            }
        }

        public Delivery()
        {
            ColModelFill();
        }

        private void ColModelFill()
        {
            ColModel = new List<string>
            {
                "ID",
                "BRAND",
                "CAUSE",
                "EMAIL",
                "CONCRETIZATION"
            };
        }
    }
}