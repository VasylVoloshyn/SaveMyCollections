using Microsoft.EntityFrameworkCore;
using MyCollection.Models;

namespace MyCollection.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MyCollectionContext context)
        {
            context.Database.Migrate();
            bool saveData = false;
            if (!context.BoneGrades.Any())
            {
                var grades = new List<BoneGrade> {
                new BoneGrade{ Code = "UNC",
                               Name = "Uncirculated",
                               Description = "Грошовий знак, що не циркулював, не має абсолютно ніяких слідів зносу або навіть наклейок. Папір чистий, без плям, кольори не змінені. Кути гострі, прямі. Немає жодного округлення, вигину чи дірочки. Без слідів обробки чи витримки під пресом. Грошовий знак у ідеальному стані. Має свій оригінальний природний блиск."
                },
                new BoneGrade{ Code = "aUNC",
                               Name = "about Uncirculated",
                               Description = "Грошовий знак, що майже не циркулював, з деяким незначним зносом. Може мати слід від мокрих рук банківського касира, або незначний згин, що піддається вирівнюванню, посередині або збоку (але не злам!). Європейці часто означають AU як EF-Unc."
                },
                new BoneGrade{ Code = "XF",
                               Name = "Extremely Fine)",
                               Description = "'Надзвичайно прекрасний стан'. Дуже привабливий знак з легким зносом. Може мати не більше трьох легких згинів, поперечних або поздовжніх (але не перетинаються!) або один сильний злам. Папір чистий, кольори не змінені, плям немає. Кути можуть мати легкі ознаки заокруглення. Знак може мати легке зношування на краю вигину."
                },
                new BoneGrade{ Code = "VF",
                               Name = "Very Fine ",
                               Description = "'Дуже прекрасний стан'. Привабливі знак, але має помітні сліди обертання. Може мати кілька поздовжніх і поперечних вигинів одночасно, але папір все ще свіжий, не старий і не «зжований». Можуть бути сліди невеликих плям. Кути злегка пошарпані, але великих заокруглень немає."
                },
                new BoneGrade{ Code = "F",
                               Name = "Fine",
                               Description = "'Прекрасний стан'. Грошовий знак, який мав довге ходіння, з великою кількістю вигинів, зламів та зморшок. Папір не дуже брудний, але може мати плями. Грані можуть мати велику обшарпаність, з невеликими надривами по краю. Надриви не простягаються углиб знака. Немає жодної дірочки в центрі через згортання. Кольори зрозумілі, але вже не дуже яскраві."
                },
                new BoneGrade{ Code = "VG",
                               Name = "Very Good",
                               Description = "'Дуже хороший стан'. Багаторазово використаний грошовий знак, але не пошкоджений. Кути можуть мати помітні ознаки зношування та округлення, а також наклейки. Надриви можуть простягатися вглиб символу, можливо зміна кольорів, може бути невелика дірочка в центрі символу від надмірного згортання. Жодні частини знака не можуть бути вирвані. Грошовий знак категорії VG може мати все ще привабливий вигляд."
                },
                new BoneGrade{ Code = "G",
                               Name = "Good",
                               Description = "'Хороший стан'. Сильно пошматований знак. Природне пошкодження від тривалого обігу може включати: сильні багаторазові вигини та злами, плями, дірочки, надриви краю, протерту дірочку в центрі, заокруглені кути та взагалі непривабливий вигляд. Жодні великі частини знака не можуть бути відсутніми. Незмивні (нестираються) сторонні написи зазвичай «зсувають» навіть нову бону в категорію Good."
                },
                new BoneGrade{ Code = "FR",
                               Name = "Fair",
                               Description = "«Горілий» грошовий знак: повністю «кульгавий» (з безліччю зморшок), брудний і дуже зношений. Великі розриви, дірочки та забруднені ділянки. Фарби подекуди повністю вигоріли."
                },
                new BoneGrade{ Code = "PR",
                               Name = "Poor ",
                               Description = "'Ганчірка'. Грошовий знак із серйозними ушкодженнями через зношування: відсутні частини поля, є незмивні написи, чорнильні плями, дірки. Може мати склеювальну стрічку, що тримає частини знака разом. Може мати доклеєні кути. У колекцію включають лише у разі надзвичайної рідкості."
                }};
                context.BoneGrades.AddRange(grades);
                saveData = true;
            }

            if (!context.CoinGrades.Any())
            {
                var coinGrades = new List<CoinGrade> {
                    new CoinGrade{ Code = "PF",
                                   Name = "Proof",
                                   Description = "Полірована. Найвища якість карбування монет, що отримується при їх виробництві із застосуванням спеціальних верстатів і особливих способів обробки заготовок і карбувального інструменту. Монети виготовляються спеціально для колекціонування, вони мають дзеркальну поверхню поля та матовий рельєф малюнка (або навпаки). За шкалою Шелдона PF 1-70"
                    },
                    new CoinGrade{ Code = "PL",
                                   Name = "Proof-like",
                                   Description = "Чудова із дзеркальним блиском. За шкалою Шелдона MS 60-70 PL"
                    },
                    new CoinGrade{ Code = "BU",
                                   Name = "Brilliant Uncirculated",
                                   Description = "За шкалою Шелдона MS 65-70"
                    },
                    new CoinGrade{ Code = "UNC",
                                   Name = "Uncirculated",
                                   Description = "Чудова. В даному стані монета повинна мати жодних ознак потертості, а деталі її малюнка зазвичай чітко помітні. У монет у цьому стані на всій площі їхньої поверхні часто присутня оригінальний «карбований» блиск. При цьому допустима присутність незначних слідів від зберігання монет у мішках у вигляді дрібних вибоїн або подряпин та деяких інших недоліків.Термін MS (скорочення від mint state), що використовується фахівцями за шкалою Шелдона, є синонімом поняття Uncirculated. За шкалою Шелдона MS 60-64"
                    },
                    new CoinGrade{ Code = "AU+",
                                   Name = "Choice Almost/About Uncirculated",
                                   Description = "Майже чудова +. За шкалою Шелдона AU 55, 58"
                    },
                    new CoinGrade{ Code = "AU",
                                   Name = "Almost/About Uncirculated",
                                   Description = "Майже чудова. За системою Шелдона AU 50, AU 53"
                    },
                    new CoinGrade{ Code = "XF+",
                                   Name = "Choice Extremely Fine",
                                   Description = "Відмінна +. За шкалою Шелдона	XF 45"
                    },
                    new CoinGrade{ Code = "XF",
                                   Name = "Extremely Fine",
                                   Description = "Відмінна. Монети в стані Extremely Fine  мають досить незначну потертість найбільш виступаючих мелких елементів малюнка. Зазвичай на таких монетах добре відрізняється не менше 90 — 95 % дрібних деталей. За шкалою Шелдона XF 40."
                    },
                    new CoinGrade{ Code = "VF+",
                                   Name = "Choice Very Fine",
                                   Description = "Майже відмінна. За шкалою Шелдона VF 30, 35"
                    },
                    new CoinGrade{ Code = "VF",
                                   Name = "Very Fine",
                                   Description = "Дуже гарна. У стані Very Fine монети вже мають досить помітну потертість, і кілька згладжених деталей малюнка (як правило, добре різниться порядок лише 75 % деталей малюнка). За шкалою Шелдона монети такої безпеки поділяють на наступні категорії: VF 20, VF 25"
                    },                    
                    new CoinGrade{ Code = "F",
                                   Name = "Fine",
                                   Description = "Гарна. Стан Fine визначає виражену потертість поверхонь внаслідок тривалого перебування монети в зверненні. Різно приблизно 50 % оригінальних деталей малюнка монети. Фахівці по Шелдоновской системі виділяють два стану: F 12 і F 15."
                    },
                    new CoinGrade{ Code = "VG",
                                   Name = "Very Good",
                                   Description = "Дуже добра. Значна потертість усієї монети. У стані Very Good, як правило, зберігається лише близько 25% початкових елементів малюнка монети. За шкалою Шелдона виділяються стани VG 8 та VG 10."
                    },
                    new CoinGrade{ Code = "G",
                                   Name = "Good",
                                   Description = "Задовільна. Дуже інтенсивна потертість монети. Зазвичай помітні переважно найбільші деталі оформлення монети. Відповідно до системи Шелдона розрізняються два ступені цього стану - G 4 і G 6."
                    },
                    new CoinGrade{ Code = "AG",
                                   Name = "Almost/About Good",
                                   Description = "Майже/Приблизно добра. По шкалі Шелдона — AG 3"
                    },
                    new CoinGrade{ Code = "FA",
                                   Name = "Fair",
                                   Description = "По шкалі Шелдона — FA 2"
                    },
                    new CoinGrade{ Code = "PR",
                                   Name = "Poor",
                                   Description = "Незадовільна. По шкалі Шелдона — PR 1"
                    }
                };
                context.CoinGrades.AddRange(coinGrades);
                saveData = true;
            }

            if (!context.StampGrades.Any())
            {
                var stampGrades = new List<StampGrade> {
                    new StampGrade{ Code = "MNH OG",
                                   Name = "Mint Never Hinged Oryginal Gum",
                                   Description = "Чиста, не використана за призначенням, що ніколи не мала наклейку, з оригінальним клеєм."
                    },
                    new StampGrade{ Code = "MNH",
                                   Name = "Mint Never Hinged",
                                   Description = "Чиста, не використана за призначенням, що ніколи не мала наклейку."
                    },
                    new StampGrade{ Code = "MLH",
                                   Name = "Mint Lightly Hinged",
                                   Description = "Чиста з пошкодженим клеєм (або акуратною наклейкою)."
                    },
                    new StampGrade{ Code = "MVLH",
                                   Name = "Mint Very Lightly Hinged",
                                   Description = "Чиста із ще менш пошкодженим клеєм."
                    },
                    new StampGrade{ Code = "MH",
                                   Name = "Mint Hinged",
                                   Description = "Чиста з пошкодженим клеєм або наклейкою (Американський термін)."
                    },
                    new StampGrade{ Code = "POG",
                                   Name = "Partial Original Gum",
                                   Description = "Частково оригінальний клей."
                    },
                    new StampGrade{ Code = "UH",
                                   Name = "Unused Hinged",
                                   Description = "Чиста з пошкодженим клеєм або наклейкою (Європейський термін)."
                    },
                    new StampGrade{ Code = "Mounted",
                                   Name = "Mounted",
                                   Description = "З наклейкою."
                    },
                    new StampGrade{ Code = "NG",
                                   Name = "No Gum",
                                   Description = "Без клею."
                    },
                    new StampGrade{ Code = "sans gomme",
                                   Name = "sans gomme",
                                   Description = "Без клею – написання у каталозі Івера."
                    },
                    new StampGrade{ Code = "o.G.",
                                   Name = "o.G.",
                                   Description = "Без клею – написання у каталозі Міхеля."
                    },
                    new StampGrade{ Code = "CTO",
                                   Name = "Cancel To Order",
                                   Description = "Гашена на замовлення – зазвичай гашені марки, не використані за призначенням, найчастіше погашені друкарським способом, призначені для продажу колекціонерам за ціною, нижчою за номінальну."
                    },
                    new StampGrade{ Code = "used",
                                   Name = "used",
                                   Description = "Використана марка (що пройшла через пошту)."
                    },
                    new StampGrade{ Code = "G/VG",
                                   Name = "Good/Very Good",
                                   Description = "Задовільний стан."
                    },
                    new StampGrade{ Code = "F",
                                   Name = "Fine",
                                   Description = "Гарний стан."
                    },
                    new StampGrade{ Code = "VF",
                                   Name = "Very Fine",
                                   Description = "Дуже гарний стан."
                    },
                    new StampGrade{ Code = "XF",
                                   Name = "Extra Fine",
                                   Description = "Відмінний стан (люкс)."
                    }

                };
                context.StampGrades.AddRange(stampGrades);
                saveData = true;
             }
                
            if (saveData)
            {
                context.SaveChanges();
            }
        }
    }
}
