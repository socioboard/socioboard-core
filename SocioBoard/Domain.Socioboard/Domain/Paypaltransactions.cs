using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
 {
    
    public class Paypaltransactions {
        public virtual Guid Id { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual float Sbamount { get; set; }
        public virtual float Clientamount { get; set; }
        public virtual string Transactionid { get; set; }
        public virtual string Correlatedid { get; set; }
        public virtual string Payerid { get; set; }
        public virtual Guid Userid { get; set; }
        public virtual bool Status { get; set; }
    }
}
