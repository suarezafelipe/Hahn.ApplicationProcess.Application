import { Router } from "aurelia-router";
import { Applicant } from "../../models/applicant";
import { ApplicantService } from "../../services/applicant/applicant-service";
import { autoinject } from "aurelia-framework";

@autoinject
export class ApplicationSuccess {
  id: number;
  applicant: Applicant;
  constructor(
    private applicantService: ApplicantService,
    private router: Router
  ) {}

  activate(params) {
    this.id = params.id;
    this.applicantService.getApplicant(this.id).then((applicant) => {
      this.applicant = applicant;
    });
  }

  createNewApplicant() {
    this.router.navigateToRoute("applicant-form");
  }
}
