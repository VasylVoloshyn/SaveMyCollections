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
            #region BoneGrades
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
            #endregion BoneGrades

            #region CoinGrades
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
            #endregion CoinGrades

            #region StampGrades
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
            #endregion StampGrades

            #region StampTypes
            if (!context.StampTypes.Any())
            {
                var stampTypes = new List<StampType>
                {
                    new StampType
                    {
                        Code ="Standard Stamp",
                        Name = "Стандартна марка",
                        Description =$"Стандартна марка, поштова марка звичайного випуску, що є частиною стандартного випуску або стандартної серії, " +
                        $"що охоплюють шкалу номіналів, достатню для покриття всіх поштових тарифів, і випускаються масовим тиражем для тривалого щоденного використання." +
                        $" Стандартні марки протиставляються комеморативним (пам'ятним) маркам, що випускаються з певних приводів.",
                        WikiLink ="https://uk.wikipedia.org/wiki/%D0%A1%D1%82%D0%B0%D0%BD%D0%B4%D0%B0%D1%80%D1%82%D0%BD%D0%B0_%D0%BC%D0%B0%D1%80%D0%BA%D0%B0    "
                    },
                    new StampType
                    {
                         Code = "Commemorative Stamp",
                         Name = "Комеморативна марка",
                         Description =$"Комеморативна марка (від фр. comme — як та memoria — пам'ять) — узагальнююча назва художніх спеціальних (пам'ятних, ювілейних та інших) поштових марок, які часто видаються на знакову дату, наприклад, річницю, для вшанування або відзначення місця, події, особи чи об'єкта. " +
                         $"Під терміном «комеморативна марка» мають на увазі крім згаданих вище також і тематичні марки (малюнки на яких присвячені певній темі колекціонування). Іноді цей термін вживається для спрощення термінології, наприклад у випадках, коли треба тільки підкреслити, " +
                         $"що марка не є загальновживаною (тобто стандартною, яка випускається поштовими адміністраціями для повсякденного масового вжитку та використовується без обмеження тиражу). Багато поштових служб щороку випускають кілька комеморативних або пам'ятних марок, " +
                         $"іноді проводячи перший день церемонії випуску у місцях, пов'язаних з предметами. Пам'ятні печатки можуть бути використані поряд з звичайними марками. На відміну від стандартних поштових марок, які часто перевидаються та продаються протягом тривалого періоду часу для загального користування, " +
                         $"ювілейні марки, як правило, друкуються в обмежених кількостях і продаються протягом набагато коротшого періоду часу, як правило, до закінчення терміну постачання. Таким чином, комеморативні марки виготовляються, як правило, відносно невеликим тиражем до певної дати на високому " +
                         $"поліграфічному рівні на відміну від стандартних поштових марок, які є чинними впродовж багатьох років,",
                         WikiLink ="https://uk.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BC%D0%B5%D0%BC%D0%BE%D1%80%D0%B0%D1%82%D0%B8%D0%B2%D0%BD%D0%B0_%D0%BC%D0%B0%D1%80%D0%BA%D0%B0"

                    },
                    new StampType
                    {
                        Code = "Miniature sheet",
                        Name = "Блок пам'ятний",
                        Description = $"Блок пам'ятний[1] (або поштовий блок) — у філателії спеціальна форма видання знаків поштової оплати, надрукованих з полями на невеликому марочному аркуші. Разом з сувенірними марочними аркушами є філателістичним сувеніром, " +
                        $"проте це не виключає можливості використовувати їх як знаки поштової оплати (на відміну від сувенірних листків).",
                        WikiLink ="https://uk.wikipedia.org/wiki/%D0%91%D0%BB%D0%BE%D0%BA_%D0%BF%D0%B0%D0%BC%27%D1%8F%D1%82%D0%BD%D0%B8%D0%B9"
                    },
                    new StampType
                    {
                        Code = "Small sheet",
                        Name = "Малий аркуш",
                        Description = $"Малий аркуш або кляйнбоген (від нім. klein — малий та Bogen — аркуш) прийняте серед філателістів визначення невеликого за форматом аркуша поштових марок. " +
                        $"Зазвичай являє собою щось середнє між поштовим блоком та повноцінним аркушем поштових марок. На відміну від звичайного, в малому аркуші відносно невелике число поштових " +
                        $"марок, що складає зазвичай від 3—4 до 10—16 (в окремих випадках і більше). У складі малого аркуша можуть бути присутні як однакові поштові марки (одного сюжету та номіналу), " +
                        $"так і різноманітні за сюжетом і номіналом. Філателістичнє поняття «малий аркуш» передбачає або існування офіційної емісії тих же самих поштових марок також і у " +
                        $"звичайних «великих» аркушах, або ту обставину, що даний аркуш містить меншу кількість марок у порівнянні зі звичайним, традиційним, «великим» марковим аркушем. " +
                        $"Поля малого аркуша іноді містять порядковий номер, малюнок та/або пам'ятний друкарський текст, що робить кляйнбоген схожим на поштовий блок, але, на відміну від останнього, " +
                        $"малий аркуш не є самостійним поштовим випуском, бо не має свого індивідуального номера в загальній нумерації, що надається у каталозі поштових марок " +
                        $"(зазвичай вказується або номер поштової марки, яка надрукована на аркуші, або номери поштових марок, що увійшли до складу малого аркуша). " +
                        $"Різниця між поштовим пам'ятним блоком та малим аркушем часом зникаюче мала: то, що в одному каталозі класифікується як «блок»," +
                        $" в іншому зустрічається як «кляйнбоген». У зв'язку з підвищенням популярності та зростанням інтересу серед філателістів до малих аркушів, " +
                        $"деякі каталоги з недавнього часу у якості додатку призводять список виданих малих аркушів з додатковою нумерацією малих аркушів." +
                        $"Історія " +
                        $"Перши кляйнбогени, у складі яких було надруковано по 25 стандартних марок номіналом 65 центів кожна, надійшли до обігу у 1919 році у Бельгії." +
                        $"Першим випуском малих аркушів пошти СРСР є комеморативна серія з двох поштових марок «6-й Міжнародний конгрес пролетарських есперантистів у" +
                        $" Ленінграді» (рос. 6-й Международный конгресс пролетарских эсперантистов в Ленинграде), що офіційно надійшли до обігу у 1926 році.Марки серії " +
                        $"було надруковано в аркушах по 10 примірників, за схємою 2×5 штук.",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9C%D0%B0%D0%BB%D0%B8%D0%B9_%D0%B0%D1%80%D0%BA%D1%83%D1%88"
                    },
                    new StampType {
                        Code = "Stamp sheet",
                        Name = "Марочний лист",
                        Description = $"Марковий лист (продажний лист, або поштовий марочний лист, або «віконцевий» лист (англ. pane, counter sheet, " +
                        $"нім. bogen, briefmarkenbogen) — частина друкарського листа з надрукованими поштовими марками, на які він розрізається в друкарні перед " +
                        $"передачею поштової адміністрації. Марочні аркуші такого розміру зручніше перевозити, зберігати, враховувати та використовувати для цілей " +
                        $"пошти Іноді марочні аркуші збігаються за розмірами з друкарськими аркушами. Марочний лист зазвичай нараховує від 25 до 400 марок залежно " +
                        $"від величини поштової марки, формату паперу та розмірів друкарської машини.У XIX столітті (у класичний період) друкарські листи марок " +
                        $"часто були марочними листами.",
                        WikiLink = "https://ru.wikipedia.org/wiki/%D0%9C%D0%B0%D1%80%D0%BE%D1%87%D0%BD%D1%8B%D0%B9_%D0%BB%D0%B8%D1%81%D1%82"
                    }
                };
                context.StampTypes.AddRange(stampTypes);
                saveData = true;
            }
            #endregion StampTypes

            #region Countries
            if (!context.Countries.Any())
            {
                var countries = new List<Country>
                {
                    new Country
                    {
                        Code = "UA",
                        Name = "Україна",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A3%D0%BA%D1%80%D0%B0%D1%97%D0%BD%D0%B0"
                    },
                    new Country
                    {
                        Code = "US",
                        Name = "Сполучені Штати Америки",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A1%D0%BF%D0%BE%D0%BB%D1%83%D1%87%D0%B5%D0%BD%D1%96_%D0%A8%D1%82%D0%B0%D1%82%D0%B8_%D0%90%D0%BC%D0%B5%D1%80%D0%B8%D0%BA%D0%B8"
                    },
                     new Country
                    {
                        Code = "GB",
                        Name = "Велика Британія",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%92%D0%B5%D0%BB%D0%B8%D0%BA%D0%B0_%D0%91%D1%80%D0%B8%D1%82%D0%B0%D0%BD%D1%96%D1%8F"
                    },
                     new Country
                    {
                        Code = "EU",
                        Name = "Європейський Союз",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%84%D0%B2%D1%80%D0%BE%D0%BF%D0%B5%D0%B9%D1%81%D1%8C%D0%BA%D0%B8%D0%B9_%D0%A1%D0%BE%D1%8E%D0%B7"
                    },
                     new Country
                    {
                        Code = "DE",
                        Name = "Німеччина",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9D%D1%96%D0%BC%D0%B5%D1%87%D1%87%D0%B8%D0%BD%D0%B0"
                    },
                     new Country
                    {
                        Code = "PL",
                        Name = "Польща",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9F%D0%BE%D0%BB%D1%8C%D1%89%D0%B0"
                    },
                     new Country
                    {
                        Code = "FR",
                        Name = "Франція",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A4%D1%80%D0%B0%D0%BD%D1%86%D1%96%D1%8F"
                    },
                     new Country
                    {
                        Code = "IT",
                        Name = "Італія",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%86%D1%82%D0%B0%D0%BB%D1%96%D1%8F"
                    },
                     new Country
                    {
                        Code = "ES",
                        Name = "Іспанія",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%86%D1%81%D0%BF%D0%B0%D0%BD%D1%96%D1%8F"
                    },
                     new Country
                    {
                        Code = "HR",
                        Name = "Хорватія",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A5%D0%BE%D1%80%D0%B2%D0%B0%D1%82%D1%96%D1%8F"
                    },
                     new Country
                    {
                        Code = "EE",
                        Name = "Естонія",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%95%D1%81%D1%82%D0%BE%D0%BD%D1%96%D1%8F"
                    },
                     new Country
                    {
                        Code = "NP",
                        Name = "Непал",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9D%D0%B5%D0%BF%D0%B0%D0%BB"
                    },
                     new Country
                     {
                         Code = "TR",
                         Name = "Туреччина",
                         WikiLink = "https://uk.wikipedia.org/wiki/%D0%A2%D1%83%D1%80%D0%B5%D1%87%D1%87%D0%B8%D0%BD%D0%B0"

                     }
                };
                context.Countries.AddRange(countries);
                saveData = true;
            }
            #endregion Countries

            if (saveData)
            {
                context.SaveChanges();
            }

            #region Currencies
            if (!context.Currencies.Any())
            {
                var countries = new List<Currency>
                {
                    new Currency
                    {
                        Code = "UAH",
                        Name = "Гривня",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="UA").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%93%D1%80%D0%B8%D0%B2%D0%BD%D1%8F"
                    },
                    new Currency
                    {
                        Code = "UAК",
                        Name = "Карбованець",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="UA").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9A%D0%B0%D1%80%D0%B1%D0%BE%D0%B2%D0%B0%D0%BD%D0%B5%D1%86%D1%8C"
                    },
                    new Currency
                    {
                        Code = "USD",
                        Name = "Dollar",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="US").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%94%D0%BE%D0%BB%D0%B0%D1%80_%D0%A1%D0%A8%D0%90"
                    },
                    new Currency
                    {
                        Code = "EUR",
                        Name = "Євро",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="EU").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%84%D0%B2%D1%80%D0%BE"
                    },
                    new Currency
                    {
                        Code = "PLN",
                        Name = "Злотий",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="PL").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%97%D0%BB%D0%BE%D1%82%D0%B8%D0%B9"
                    },
                    new Currency
                    {
                        Code = "TRY",
                        Name = "Турецька ліра",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="PL").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A2%D1%83%D1%80%D0%B5%D1%86%D1%8C%D0%BA%D0%B0_%D0%BB%D1%96%D1%80%D0%B0"
                    },
                };
                context.Currencies.AddRange(countries);
                context.SaveChanges();
            }
            #endregion Currencies

            #region Dimes
            if (!context.Dimes.Any())
            {
                var dimes = new List<Dime>
                {
                    new Dime
                    {
                        Code = "UAH K",
                        Name = "Копійка",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="UA").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BF%D1%96%D0%B9%D0%BA%D0%B0"
                    },
                    new Dime
                    {
                        Code = "UAH H",
                        Name = "Гривня",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="UA").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%93%D1%80%D0%B8%D0%B2%D0%BD%D1%8F"
                    },
                    new Dime
                    {
                        Code = "USD C",
                        Name = "Cent",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="US").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A6%D0%B5%D0%BD%D1%82_(%D0%B3%D1%80%D0%BE%D1%88%D1%96)"
                    },
                     new Dime
                    {
                        Code = "USD D",
                        Name = "Dollar",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="US").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%94%D0%BE%D0%BB%D0%B0%D1%80_%D0%A1%D0%A8%D0%90"
                    },
                    new Dime
                    {
                        Code = "EUR C",
                        Name = "Євроцент",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="EU").Select(c=>c.Id).First(),
                        WikiLink = "https://ru.wikipedia.org/wiki/%D0%95%D0%B2%D1%80%D0%BE%D1%86%D0%B5%D0%BD%D1%82"
                    },
                    new Dime
                    {
                        Code = "EUR E",
                        Name = "Євро",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="EU").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9C%D0%BE%D0%BD%D0%B5%D1%82%D0%B8_%D1%94%D0%B2%D1%80%D0%BE"
                    },
                    new Dime
                    {
                        Code = "PLN",
                        Name = "Грош",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="PL").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%93%D1%80%D0%BE%D1%88"
                    },
                    new Dime
                    {
                        Code = "TRY",
                        Name = "Куруш",
                        CountryId = context.Countries.AsNoTracking().Where(c=>c.Code=="PL").Select(c=>c.Id).First(),
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9A%D1%83%D1%80%D1%83%D1%88_(%D0%BC%D0%BE%D0%BD%D0%B5%D1%82%D0%B0)"
                    },
                };
                context.Dimes.AddRange(dimes);
                context.SaveChanges();
            }
            #endregion Dimes

            #region Persons
            if (!context.Persons.Any())
            {
                var persons = new List<Person>
                {
                    new Person
                    {
                        Name = "Володимир",
                        FamilyName = "Матвієнко",
                        FatherName = "Павлович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9C%D0%B0%D1%82%D0%B2%D1%96%D1%94%D0%BD%D0%BA%D0%BE_%D0%92%D0%BE%D0%BB%D0%BE%D0%B4%D0%B8%D0%BC%D0%B8%D1%80_%D0%9F%D0%B0%D0%B2%D0%BB%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Вадим",
                        FamilyName = "Гетьман",
                        FatherName = "Петрович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%93%D0%B5%D1%82%D1%8C%D0%BC%D0%B0%D0%BD_%D0%92%D0%B0%D0%B4%D0%B8%D0%BC_%D0%9F%D0%B5%D1%82%D1%80%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Віктор",
                        FamilyName = "Ющенко",
                        FatherName = "Андрійович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%AE%D1%89%D0%B5%D0%BD%D0%BA%D0%BE_%D0%92%D1%96%D0%BA%D1%82%D0%BE%D1%80_%D0%90%D0%BD%D0%B4%D1%80%D1%96%D0%B9%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Володимир",
                        FamilyName = "Стельмах",
                        FatherName = "Семенович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A1%D1%82%D0%B5%D0%BB%D1%8C%D0%BC%D0%B0%D1%85_%D0%92%D0%BE%D0%BB%D0%BE%D0%B4%D0%B8%D0%BC%D0%B8%D1%80_%D0%A1%D0%B5%D0%BC%D0%B5%D0%BD%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Сергій",
                        FamilyName = "Тігіпко",
                        FatherName = "Леонідович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A2%D1%96%D0%B3%D1%96%D0%BF%D0%BA%D0%BE_%D0%A1%D0%B5%D1%80%D0%B3%D1%96%D0%B9_%D0%9B%D0%B5%D0%BE%D0%BD%D1%96%D0%B4%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Сергій",
                        FamilyName = "Арбузов",
                        FatherName = "Геннадійович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%90%D1%80%D0%B1%D1%83%D0%B7%D0%BE%D0%B2_%D0%A1%D0%B5%D1%80%D0%B3%D1%96%D0%B9_%D0%93%D0%B5%D0%BD%D0%BD%D0%B0%D0%B4%D1%96%D0%B9%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Ігор",
                        FamilyName = "Соркін",
                        FatherName = "В'ячеславович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A1%D0%BE%D1%80%D0%BA%D1%96%D0%BD_%D0%86%D0%B3%D0%BE%D1%80_%D0%92%27%D1%8F%D1%87%D0%B5%D1%81%D0%BB%D0%B0%D0%B2%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                    new Person
                    {
                        Name = "Степан",
                        FamilyName = "Кубів",
                        FatherName = "Іванович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%9A%D1%83%D0%B1%D1%96%D0%B2_%D0%A1%D1%82%D0%B5%D0%BF%D0%B0%D0%BD_%D0%86%D0%B2%D0%B0%D0%BD%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                     new Person
                    {
                        Name = "Валерія",
                        FamilyName = "Гонтарева",
                        FatherName = "Олексіївна",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%93%D0%BE%D0%BD%D1%82%D0%B0%D1%80%D0%B5%D0%B2%D0%B0_%D0%92%D0%B0%D0%BB%D0%B5%D1%80%D1%96%D1%8F_%D0%9E%D0%BB%D0%B5%D0%BA%D1%81%D1%96%D1%97%D0%B2%D0%BD%D0%B0"
                    },
                     new Person
                    {
                        Name = "Яків",
                        FamilyName = "Смолій",
                        FatherName = "Васильович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A1%D0%BC%D0%BE%D0%BB%D1%96%D0%B9_%D0%AF%D0%BA%D1%96%D0%B2_%D0%92%D0%B0%D1%81%D0%B8%D0%BB%D1%8C%D0%BE%D0%B2%D0%B8%D1%87"
                    },
                      new Person
                    {
                        Name = "Кирило",
                        FamilyName = "Шевченко",
                        FatherName = "Євгенович",
                        WikiLink = "https://uk.wikipedia.org/wiki/%D0%A8%D0%B5%D0%B2%D1%87%D0%B5%D0%BD%D0%BA%D0%BE_%D0%9A%D0%B8%D1%80%D0%B8%D0%BB%D0%BE_%D0%84%D0%B2%D0%B3%D0%B5%D0%BD%D0%BE%D0%B2%D0%B8%D1%87"
                    }
                };
                context.Persons.AddRange(persons);
                context.SaveChanges();
            }
            #endregion Persons

            #region Signatures
            if (!context.Signatures.Any())
            {
                var signatures = new List<Signature>
                {
                    new Signature
                    { 
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Матвієнко").Select(c=>c.Id).First(),
                        Note = "Час на посаді 6 червня 1991 - 24 березня 1992"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Гетьман").Select(c=>c.Id).First(),
                        Note = "Час на посаді 24 березня 1992 — 18 грудня 1992"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Ющенко").Select(c=>c.Id).First(),
                        Note = "Час на посаді 26 січня 1993 — 11 січня 2000"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Стельмах").Select(c=>c.Id).First(),
                        Note = "Час на посаді 21 січня 2000 — 17 грудня 2002"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Тігіпко").Select(c=>c.Id).First(),
                        Note = "Час на посаді 17 грудня 2002 — 16 грудня 2004"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Стельмах").Select(c=>c.Id).First(),
                        Note = "Час на посаді 16 грудня 2004 — 23 грудня 2010"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Арбузов").Select(c=>c.Id).First(),
                        Note = "Час на посаді 23 грудня 2010 — 11 січня 2013"
                    },
                    new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Соркін").Select(c=>c.Id).First(),
                        Note = "Час на посаді 11 січня 2013 — 24 лютого 2014"
                    },
                     new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Кубів").Select(c=>c.Id).First(),
                        Note = "Час на посаді 24 лютого 2014  — 19 червня 2014"
                    },
                     new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Гонтарева").Select(c=>c.Id).First(),
                        Note = "Час на посаді 19 червня 2014  — 15 березня 2018"
                    },
                      new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Смолій").Select(c=>c.Id).First(),
                        Note = "Час на посаді 15 березня 2018  — 13 липня 2020"
                    },
                      new Signature
                    {
                        PersonId = context.Persons.AsNoTracking().Where(p=>p.FamilyName=="Шевченко").Select(c=>c.Id).First(),
                        Note = "Час на посаді з 16 липня 2020"
                    },

                };
                context.Signatures.AddRange(signatures);
                context.SaveChanges();
            }
            #endregion Signatures
        }
    }
}