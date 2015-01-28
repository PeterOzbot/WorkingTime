using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Library.Calendar {
    /// <summary>
    /// Collection of months
    /// </summary>
    public class Year : IEnumerable<Month> {
        // private fields for months
        private Month _january = new Month("January", 1);
        private Month _february = new Month("February", 2);
        private Month _march = new Month("March", 3);
        private Month _april = new Month("April", 4);
        private Month _may = new Month("May", 5);
        private Month _june = new Month("June", 6);
        private Month _july = new Month("July", 7);
        private Month _august = new Month("August", 8);
        private Month _september = new Month("September", 9);
        private Month _october = new Month("October", 10);
        private Month _november = new Month("November", 11);
        private Month _december = new Month("December", 12);

        /// <summary>
        /// Year index ie. 2015
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Month on specific index in this year
        /// </summary>
        public Month this[int index] {
            get {
                switch (index) {
                    case 1: return January;
                    case 2: return February;
                    case 3: return March;
                    case 4: return April;
                    case 5: return May;
                    case 6: return June;
                    case 7: return July;
                    case 8: return August;
                    case 9: return September;
                    case 10: return October;
                    case 11: return November;
                    case 12: return December;
                    default:
                        throw new ArgumentOutOfRangeException("Index must be 1 or more.");

                }
            }
        }

        // months
        public Month January { get { return _january; } }
        public Month February { get { return _february; } }
        public Month March { get { return _march; } }
        public Month April { get { return _april; } }
        public Month May { get { return _may; } }
        public Month June { get { return _june; } }
        public Month July { get { return _july; } }
        public Month August { get { return _august; } }
        public Month September { get { return _september; } }
        public Month October { get { return _october; } }
        public Month November { get { return _november; } }
        public Month December { get { return _december; } }




        /// <summary>
        /// Initializes specific year
        /// </summary>
        public Year(int year) {
            Index = year;

            FillMonths();
        }



        /// <summary>
        /// Initializes collection of months
        /// </summary>
        private void FillMonths() {

            foreach (Month month in this) {
                month.Fill(Index);
            }
        }


        #region IEnumerable<Month> Members
        public IEnumerator<Month> GetEnumerator() {
            yield return January;
            yield return February;
            yield return March;
            yield return April;
            yield return May;
            yield return June;
            yield return July;
            yield return August;
            yield return September;
            yield return October;
            yield return November;
            yield return December;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
        #endregion
    }
}
