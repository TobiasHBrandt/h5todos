using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace h5todos.Models
{
    public class TodosItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; }
        public bool IsDone { get; set; }
        public int loginId { get; set; }
    }
}
