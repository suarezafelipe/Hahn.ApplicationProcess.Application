import { observable, autoinject } from "aurelia-framework";
import { DialogService } from "aurelia-dialog";
import { EventAggregator, Subscription } from "aurelia-event-aggregator";
import { HttpClient } from "aurelia-fetch-client";
import { I18N } from "aurelia-i18n";
import { Router } from "aurelia-router";
import {
  ValidationRules,
  ValidationControllerFactory,
  ValidationController,
} from "aurelia-validation";
import { ApplicantService } from "services/applicant/applicant-service";
import { BootstrapFormRenderer } from "../../resources/bootstrap-form-renderer";
import { Dialog } from "../../components/dialog";
import isJson from "../../services/common/isJsonUtil";
import flattenObjOfArr from "../../services/common/flattenObjectOfArrays";

@autoinject
export class ApplicantForm {
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
  loading = false;

  constructor(
    ValidationControllerFactory: ValidationControllerFactory,
    DialogService: DialogService,
    private i18n: I18N,
    private ea: EventAggregator,
    private httpClient: HttpClient,
    private applicantService: ApplicantService,
    private router: Router
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

  created() {
    this.setValidation();
  }

  submit() {
    this.loading = true;
    this.applicantService
      .createApplicant({
        name: this.name,
        familyName: this.familyName,
        address: this.address,
        countryOfOrigin: this.countryOfOrigin,
        emailAddress: this.email,
        age: this.age,
        hired: this.hired,
      })
      .then((result) => {
        this.loading = false;
        this.router.navigateToRoute("confirmation", { id: result.id });
      })
      .catch((error: Error) => {
        this.loading = false;
        if (isJson(error.message)) {
          this.dialogService.open({
            viewModel: Dialog,
            model: {
              listOfMessages: flattenObjOfArr(JSON.parse(error.message)),
              title: this.i18n.tr("errorDialog.title"),
            },
          });
        } else {
          this.dialogService.open({
            viewModel: Dialog,
            model: {
              message: this.i18n.tr("errorDialog.description"),
              title: this.i18n.tr("errorDialog.title"),
            },
          });
        }
      });
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

  countryOfOriginChanged() {
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
      .ensure("countryOfOrigin")
      .required()
      .withMessage(this.i18n.tr("applicantForm.countryRequiredValidation"))
      .satisfies((country) => this.validateCountry(country))
      .withMessage(this.i18n.tr("applicantForm.countryValidation"))
      .on(ApplicantForm);
  }

  async validateCountry(country: string) {
    return await this.httpClient
      .fetch(`https://restcountries.eu/rest/v2/name/${country}?fullText=true`)
      .then(({ status }) => status === 200)
      .catch(() => false);
  }
}
