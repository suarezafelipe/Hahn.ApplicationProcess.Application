import { observable, autoinject } from "aurelia-framework";
import {
  ValidationRules,
  ValidationControllerFactory,
  ValidationController,
} from "aurelia-validation";
import { DialogService } from "aurelia-dialog";
import { Dialog } from "../../components/dialog";
import { BootstrapFormRenderer } from "../../resources/bootstrap-form-renderer";
import { I18N } from "aurelia-i18n";
import { EventAggregator, Subscription } from "aurelia-event-aggregator";

@autoinject
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
  dialogService: DialogService;
  localeSubscription: Subscription;

  constructor(
    ValidationControllerFactory: ValidationControllerFactory,
    DialogService: DialogService,
    private i18n: I18N,
    private ea: EventAggregator
  ) {
    this.controller = ValidationControllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.isApplicantComplete = false;
    this.isEmptyApplicant = true;
    this.dialogService = DialogService;
    this.localeSubscription = this.ea.subscribe("locale-changed", () => {
      this.setValidation();
    });
  }

  attached(): void {}

  created() {
    this.setValidation();
  }

  setValidation() {
    ValidationRules.ensure("name")
      .required()
      .withMessage(this.i18n.tr("applicantForm.nameRequiredValidation"))
      .minLength(5)
      .withMessage(this.i18n.tr("applicantForm.nameLengthValidation"))
      .ensure("familyName")
      .required()
      .withMessage(this.i18n.tr("applicantForm.familyNameRequiredValidation"))
      .minLength(5)
      .withMessage(this.i18n.tr("applicantForm.familyNameLengthValidation"))
      .ensure("address")
      .required()
      .withMessage(this.i18n.tr("applicantForm.addressRequiredValidation"))
      .minLength(10)
      .withMessage(this.i18n.tr("applicantForm.addressLengthValidation"))
      .ensure("email")
      .required()
      .withMessage(this.i18n.tr("applicantForm.emailRequiredValidation"))
      .email()
      .withMessage(this.i18n.tr("applicantForm.emailValidation"))
      .ensure("age")
      .required()
      .withMessage(this.i18n.tr("applicantForm.ageRequiredValidation"))
      .range(20, 60)
      .withMessage(this.i18n.tr("applicantForm.ageRangeValidation"))
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

  openDialog(): void {
    const self = this;
    this.dialogService.open({
      viewModel: Dialog,
      model: {
        message: this.i18n.tr("resetDialog.description"),
        title: this.i18n.tr("resetDialog.title"),
        action: this.action.bind(self),
      },
    });
  }

  action(): void {
    this.reset();
  }

  detached() {
    this.localeSubscription.dispose();
  }
}
