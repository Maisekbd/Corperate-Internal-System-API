using DocumentsExplorer.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model
{
    class DocumentExplorerMigrationConfiguration : DbMigrationsConfiguration<DocumentsExplorerContext>
    {
        public DocumentExplorerMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        //protected override void Seed(DocumentsExplorerContext context)
        //{
        //    var councilTypeCount = context.CouncilTypes.Count();
        //    if (councilTypeCount == 0)
        //    {
        //        IList<CouncilType> CouncilTypes = new List<CouncilType>();

        //        CouncilTypes.Add(new CouncilType() {  Description = "مجلس الإدارة" });
        //        CouncilTypes.Add(new CouncilType() { Description = "مجلس المساهمين" });
        //        CouncilTypes.Add(new CouncilType() { Description = "اللجنة التنفيذية" });

        //        context.CouncilTypes.AddRange(CouncilTypes);
        //    }


        //    var CountriesCount = context.Countries.Count();
        //    if (CountriesCount == 0)
        //    {
        //        IList<Country> Countries = new List<Country>();

        //        Countries.Add(new Country() { Name = "عمان" });
        //        Countries.Add(new Country() { Name = "الإمارات" });
        //        Countries.Add(new Country() { Name = "البحرين" });
        //        Countries.Add(new Country() { Name = "السعودية" });
        //        Countries.Add(new Country() { Name = "الكويت" });
        //        Countries.Add(new Country() { Name = "اليمن" });
        //        Countries.Add(new Country() { Name = "الأردن" });
        //        Countries.Add(new Country() { Name = "العراق" });
        //        Countries.Add(new Country() { Name = "سوريا" });
        //        Countries.Add(new Country() { Name = "فلسطين" });
        //        Countries.Add(new Country() { Name = "لبنان" });
        //        Countries.Add(new Country() { Name = "مصر" });
        //        Countries.Add(new Country() { Name = "السودان" });
        //        Countries.Add(new Country() { Name = "ليبيا" });
        //        Countries.Add(new Country() { Name = "تونس" });
        //        Countries.Add(new Country() { Name = "المغرب" });
        //        Countries.Add(new Country() { Name = "الجزائر" });
        //        Countries.Add(new Country() { Name = "موريتانيا" });
        //        Countries.Add(new Country() { Name = "جزر القمر" });
        //        Countries.Add(new Country() { Name = "جيبوتي" });
        //        Countries.Add(new Country() { Name = "الصومال" });

        //        context.Countries.AddRange(Countries);
        //    }

        //    var MainCategoriesCount = context.MainCategories.Count();
        //    if (MainCategoriesCount == 0)
        //    {
        //        IList<MainCategory> mainCategories = new List<MainCategory>();
        //        /// مجلس الإدارة
        //        var firstCouncilId = context.CouncilTypes.Where(c => c.Description.Trim() == "مجلس الإدارة").FirstOrDefault().Id;
        //        mainCategories.Add(new MainCategory() { Description = "عــــام", CouncilTypeId= firstCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "المشروعــات", CouncilTypeId = firstCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الشئون المالــية", CouncilTypeId = firstCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الشؤون الإدارية", CouncilTypeId = firstCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الشؤون القانونية", CouncilTypeId = firstCouncilId });

        //        /// مجلس المساهمين
        //        var secondCouncilId = context.CouncilTypes.Where(c => c.Description.Trim() == "مجلس المساهمين").FirstOrDefault().Id;
        //        mainCategories.Add(new MainCategory() { Description = "أعضاء مجلس الإدارة", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "رئيس الهيئة", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الدول الأعضاء", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الانتخاب", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "شركات ومؤسسات", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "مدققي الحسابات", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الميزانية", CouncilTypeId = secondCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "الخطة الاستثمارية", CouncilTypeId = secondCouncilId });

        //        /// اللجنة التنفيذية
        //        var thirdCouncilId = context.CouncilTypes.Where(c => c.Description.Trim() == "اللجنة التنفيذية").FirstOrDefault().Id;
        //        mainCategories.Add(new MainCategory() { Description = "عــــام", CouncilTypeId = thirdCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "التوصية للمجلس", CouncilTypeId = thirdCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "موافقات", CouncilTypeId = thirdCouncilId });
        //        mainCategories.Add(new MainCategory() { Description = "إرجاء النظر", CouncilTypeId = thirdCouncilId });

        //        context.MainCategories.AddRange(mainCategories);


        //    }

        //    base.Seed(context);
        //}
    }
}
