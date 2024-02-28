﻿using FluentValidation;
using Unite.Donors.Feed.Web.Models.Base;
using Unite.Donors.Feed.Web.Models.Base.Validators;

namespace Unite.Donors.Feed.Web.Models.Validators;

public class DonorDataModelValidator : AbstractValidator<DonorDataModel>
{
    private readonly IValidator<TreatmentModel> _treatmentModelValidator = new TreatmentModelValidator();
    private readonly IValidator<ClinicalDataModel> _clinicalDataModelValidator = new ClinicalDataModelValidator();

    public DonorDataModelValidator()
    {
        RuleFor(model => model.Id)
            .NotEmpty()
            .WithMessage("Should not be empty");

        RuleFor(model => model.Id)
            .MaximumLength(255)
            .WithMessage("Maximum length is 255");

        RuleForEach(model => model.Projects)
            .NotEmpty()
            .WithMessage("Should not have empty values");

        RuleForEach(model => model.Projects)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");

        RuleForEach(model => model.Studies)
            .NotEmpty()
            .WithMessage("Should not have empty values");

        RuleForEach(model => model.Studies)
            .MaximumLength(100)
            .WithMessage("Maximum length is 100");

        RuleFor(model => model.ClinicalData)
            .SetValidator(_clinicalDataModelValidator);

        RuleForEach(model => model.Treatments)
            .SetValidator(_treatmentModelValidator);
    }
}


public class DonorDataModelsValidator : AbstractValidator<DonorDataModel[]>
{
    private readonly IValidator<DonorDataModel> _modelValidator = new DonorDataModelValidator();

    public DonorDataModelsValidator()
    {
        RuleFor(models => models)
            .Must(models => models.Any())
            .WithMessage("Should not be empty");

        RuleForEach(models => models)
            .SetValidator(_modelValidator);
    }
}
