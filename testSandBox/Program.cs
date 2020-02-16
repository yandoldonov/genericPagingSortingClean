using dbPersistance;
using dbPersistance.enums;
using dbPersistance.uOw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testSandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            //Random rnd = new Random();

            //int startValue = 0;
            //int maxValue = 0;

            //while (maxValue <= 100000)
            //{
            //    Console.WriteLine("processing nex badge");

            //    if (maxValue > 0)
            //    {
            //        startValue = maxValue;
            //    }

            //    maxValue = startValue + 1000;

            //    using (unitOfWork<dbItemTypeOne> unit = new unitOfWork<dbItemTypeOne>())
            //    {

            //        for (int i = startValue; i < maxValue; i++)
            //        {
            //            unit.repository.Insert(new dbItemTypeOne()
            //            {
            //                guid = Guid.NewGuid().ToString(),
            //                name = rnd.Next(1, 100000) + "-name-" + rnd.Next(1, 100000),
            //                decimalData = rnd.Next(1, 100000),
            //                description = rnd.Next(1, 100000) + "descripiton - " + rnd.Next(1, 100000),
            //                boolvalue = getBoolValue(rnd.Next(1, 100000))
            //            });

            //            Console.WriteLine("processing item #: " + i);
            //        }

            //        unit.Save();
            //    }

            //    using (unitOfWork<dbItemTypeTwo> unit = new unitOfWork<dbItemTypeTwo>())
            //    {

            //        for (int i = startValue; i < maxValue; i++)
            //        {
            //            unit.repository.Insert(new dbItemTypeTwo()
            //            {
            //                guid = Guid.NewGuid().ToString(),
            //                name = rnd.Next(1, 100000) + "-name-" + rnd.Next(1, 100000),
            //                decValue = rnd.Next(1, 100000),
            //                description = rnd.Next(1, 100000) + "descripiton - " + rnd.Next(1, 100000),
            //                stringValueOne = rnd.Next(1, 100000) + " - stringValueOne - " + rnd.Next(1, 100000),
            //                stringValueTwo = rnd.Next(1, 100000) + " - stringValueTwo - " + rnd.Next(1, 100000),
            //                intVlue = rnd.Next(1, 100000),
            //                invFieldTwo = rnd.Next(1, 100000)
            //            });

            //            Console.WriteLine("processing item #: " + i);
            //        }

            //        unit.Save();
            //    }

            //    DateTime start = new DateTime(1995, 1, 1);
            //    int range = (DateTime.Today - start).Days;

            //    using (unitOfWork<dbItemTypeThree> unit = new unitOfWork<dbItemTypeThree>())
            //    {

            //        for (int i = startValue; i < maxValue; i++)
            //        {
            //            unit.repository.Insert(new dbItemTypeThree()
            //            {
            //                guid = Guid.NewGuid().ToString(),
            //                name = rnd.Next(1, 100000) + "-name-" + rnd.Next(1, 100000),
            //                description = rnd.Next(1, 100000) + "descripiton - " + rnd.Next(1, 100000),
            //                dataTypeEnum = (dataTypeEnum)rnd.Next(0, 3),
            //                indicatedDate = start.AddDays(rnd.Next(range))
            //            });

            //            Console.WriteLine("processing item #: " + i);
            //        }

            //        unit.Save();
            //    }
            //}

            linkItems();
        }

        static bool getBoolValue(int value)
        {
            if (value % 2 == 0) return true;
            else return false;
        }

        static void linkItems()
        {
            int totalItemOneCount = 0;
            int totalItemTwoCount = 0;
            int totalItemTreeCount = 0;

            using (uOfWork unit = new uOfWork())
            {
                totalItemOneCount = unit.dbItemTypeOne.Count();
                totalItemTwoCount = unit.dbItemTypeTwo.Count();
                totalItemTreeCount = unit.dbItemTypeThree.Count();
            }

            Random rnd = new Random();
            int stepCount = 1;

            while(stepCount <= totalItemTwoCount)
            {
                using (uOfWork unit = new uOfWork())
                {
                    Console.WriteLine($"processing batch beginning at: {stepCount}");

                    for(int i = stepCount; i < stepCount + 1000; i++)
                    {
                       var item = unit.dbItemTypeTwo.GetByID(i);

                        if(item != null)
                        {
                            var linkedItem = unit.dbItemTypeOne.GetByID(rnd.Next(1, totalItemOneCount));

                            if (linkedItem != null)
                            {
                                Console.WriteLine($"linking item: {linkedItem.Id} to item {item.Id}");
                                item.dbItemTypeOne = linkedItem;
                            }
                            unit.dbItemTypeTwo.Update(item);
                        }                     
                    }

                    unit.Save();
                }

                stepCount = stepCount + 1000;
            }

        }
    }
}
