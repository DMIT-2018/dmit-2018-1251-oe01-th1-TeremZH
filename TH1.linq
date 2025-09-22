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

ClubActivities
    .Where(activity => 
        activity.StartDate.HasValue &&
        activity.StartDate.Value.Date >= new DateTime(2025, 1, 1).Date &&
        activity.Name != "BTech Club Meeting" &&
        (activity.OffCampus || activity.CampusVenue.Location != "Scheduled Room")
    )
    .Select(activity => new 
    {
        StartDate = activity.StartDate.Value.ToString("yyyy-MM-dd h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),
        Location = activity.OffCampus ? activity.Location : activity.CampusVenue.Location,
        Club = activity.Club.ClubName,
        Activity = activity.Name
    })
    .OrderBy(a => a.StartDate)
    .Dump();