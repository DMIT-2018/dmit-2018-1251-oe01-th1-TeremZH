<Query Kind="Statements">
  <Connection>
    <ID>061bc4cf-fb38-4f03-8e5b-8121a26ac421</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Server>Terem\SQLEXPRESS </Server>
    <Database>StartTed-2025-Sep</Database>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

/*Question1*/
ClubActivities
    .Where(activity => 
        activity.StartDate.HasValue &&
        activity.StartDate.Value.Date >= new DateTime(2025, 1, 1).Date &&
        activity.Name != "BTech Club Meeting" &&
        (activity.OffCampus || activity.CampusVenue.Location != "Scheduled Room")
    )
	.OrderBy(a => a.StartDate)
    .Select(activity => new 
    {
        StartDate = activity.StartDate.Value.ToString("yyyy-MM-dd h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
        Location = activity.OffCampus ? activity.Location : activity.CampusVenue.Location,
        Club = activity.Club.ClubName,
        Activity = activity.Name
    })
    .Dump();

/*Question2*/
Programs
	.Where(p => p.ProgramCourses.Count(pc => pc.Required) >= 22)
    .Select(p => new 
    {
        School = p.SchoolCode == "SAMIT" ? "School of Advance Media and IT" 
               : p.SchoolCode == "SEET" ? "School of Electrical Engineering Technology" 
               : "Unknown",
        Program = p.ProgramName,
        RequiredCourseCount = p.ProgramCourses.Count(pc => pc.Required),
        OptionalCourseCount = p.ProgramCourses.Count(pc => !pc.Required)
    })
	.OrderBy(p => p.Program)
    .Dump();

/*Question3*/
Students
    .Where(s => !s.StudentPayments.Any() && s.Countries.CountryName != "Canada")
    .OrderBy(s => s.LastName)
    .Select(s => new 
    {
        StudentNumber = s.StudentNumber,
        CountryName = s.Countries.CountryName,
        FullName = s.FirstName + " " + s.LastName,
        ClubMembershipCount = s.ClubMembers.Any() ? s.ClubMembers.Count().ToString() : "None"
    })
    .Dump();

/*Question4*/
