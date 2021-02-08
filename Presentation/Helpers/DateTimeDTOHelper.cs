using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace Presentation.Helpers
{
    public static class DateTimeDTOHelper
    {
        public static List<PageViewStatsDTO> SetTimeZone(IEnumerable<PageViewStatsDTO> data, string timeZoneName)
        {
            var listOfDtos = data.ToList();
            for (int i = 0; i < listOfDtos.Count; i++)
            {
                TimeZoneInfo cstZone = TZConvert.GetTimeZoneInfo(timeZoneName);
                listOfDtos[i].CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(listOfDtos[i].CreatedAt, cstZone);
            }

            return listOfDtos;
        }
    }
}
