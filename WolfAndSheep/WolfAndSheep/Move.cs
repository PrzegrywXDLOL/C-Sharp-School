using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfAndSheep {
    public class Move 
    {
        public int Id { get; set; }
        public int BeforeRow {  get; set; }
        public int BeforeCol {  get; set; }
        public int AfterRow {  get; set; }
        public int AfterCol {  get; set; }
    }
}
