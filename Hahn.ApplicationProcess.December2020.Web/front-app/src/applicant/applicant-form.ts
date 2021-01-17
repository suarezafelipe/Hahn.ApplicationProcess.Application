import { observable, inject } from "aurelia-framework";
import {
  ValidationRules,
  ValidationControllerFactory,
  ValidationController,
} from "aurelia-validation";

@inject(ValidationControllerFactory)
export class ApplicantForm {
  public message = "hello from applicant form";

  @observable name: string;
  @observable familyName: string;
  @observable address: string;
  @observable countryOfOrigin: string;
  @observable email: string;
  @observable age: number;
  @observable hired: boolean;

  controller: ValidationController;
  isEmptyApplicant: boolean;
  isApplicantComplete: boolean;

  constructor(ValidationControllerFactory) {
    this.controller = ValidationControllerFactory.createForCurrentScope();
    this.isApplicantComplete = false;
    this.isEmptyApplicant = true;
  }

  created() {
    ValidationRules.ensure("name")
      .required()
      .minLength(5)
      .ensure("familyName")
      .required()
      .minLength(5)
      .ensure("address")
      .required()
      .minLength(10)
      .ensure("email")
      .required()
      .email()
      .ensure("age")
      .required()
      .range(20, 60)
      .on(ApplicantForm);
  }

  submit() {
    console.log("submit triggered!");
    console.log(this.name);
    console.log(this.age);
    console.log(this.hired);
  }

  nameChanged() {
    this.isEmptyApplicant = this.isFormEmpty();
    this.isApplicantComplete = this.isFormFilled();
  }

  familyNameChanged() {
    this.isEmptyApplicant = this.isFormEmpty();
    this.isApplicantComplete = this.isFormFilled();
  }

  emailChanged() {
    this.isEmptyApplicant = this.isFormEmpty();
    this.isApplicantComplete = this.isFormFilled();
  }

  addressChanged() {
    this.isEmptyApplicant = this.isFormEmpty();
    this.isApplicantComplete = this.isFormFilled();
  }

  ageChanged() {
    this.isEmptyApplicant = this.isFormEmpty();
    this.isApplicantComplete = this.isFormFilled();
  }

  isFormEmpty() {
    let isEmpty = true;

    if (
      this.name ||
      this.familyName ||
      this.address ||
      this.countryOfOrigin ||
      this.email ||
      this.age
    ) {
      isEmpty = false;
    }

    return isEmpty;
  }

  isFormFilled() {
    let isFilled = false;

    if (
      this.name &&
      this.familyName &&
      this.address &&
      this.countryOfOrigin &&
      this.email &&
      this.age
    ) {
      isFilled = true;
    }

    return isFilled;
  }

  reset() {
    this.name = "";
    this.familyName = "";
    this.countryOfOrigin = "";
    this.address = "";
    this.email = "";
    this.age = null;
    this.hired = false;
  }
}
