﻿namespace Unite.Donors.Feed.Data.Models;

public class TreatmentModel
{
    public string Therapy { get; set; }
    public string Details { get; set; }
    public DateOnly? StartDate { get; set; }
    public int? StartDay { get; set; }
    public DateOnly? EndDate { get; set; }
    public int? DurationDays { get; set; }
    public string Results { get; set; }
}
