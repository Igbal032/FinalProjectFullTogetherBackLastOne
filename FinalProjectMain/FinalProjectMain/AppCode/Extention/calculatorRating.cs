using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain
{
    public static class calculatorRating
    {
        public static int calStar(int starCount,int totalStarLevel)
        {

            int answer = totalStarLevel / starCount;
            return answer;
        }
    }
}