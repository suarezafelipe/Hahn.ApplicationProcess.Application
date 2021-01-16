import { bindable, inject } from "aurelia-framework";
import {
  ValidationRules,
  ValidationControllerFactory,
  ValidationController,
} from "aurelia-validation";

interface Applicant {
  name: string;
  familyName: string;
  address: string;
  countryOfOrigin: string;
  email: string;
  age: number;
  hired: boolean;
}

@inject(ValidationControllerFactory)
export class ApplicantForm {
  public message = "hello from applicant form";
  @bindable applicant: Applicant;
  pageJustLoaded: boolean;
  controller: ValidationController;
  isEmptyApplicant: boolean;

  constructor(ValidationControllerFactory) {
    this.controller = ValidationControllerFactory.createForCurrentScope();
    this.pageJustLoaded = true;
    this.isEmptyApplicant = true;
  }

  submit() {
    this.controller.validate();

    console.log(this.applicant);
    // To prevent default browser's behaviour of reloading the page
    return false;
  }

  applicantChanged() {
    this.controller.validate();
    this.pageJustLoaded = false;
    if (this.applicant) {
      ValidationRules.ensure((a: Applicant) => a.name)
        .required()
        .minLength(5)
        .ensure((a: Applicant) => a.familyName)
        .required()
        .minLength(5)
        .ensure((a: Applicant) => a.address)
        .required()
        .minLength(10)
        .ensure((a: Applicant) => a.email)
        .required()
        .email()
        .ensure((a: Applicant) => a.age)
        .required()
        .range(20, 60)
        .on(this.applicant);
      this.isEmptyApplicant = false;
    } else {
      this.isEmptyApplicant = true;
    }
  }
}
