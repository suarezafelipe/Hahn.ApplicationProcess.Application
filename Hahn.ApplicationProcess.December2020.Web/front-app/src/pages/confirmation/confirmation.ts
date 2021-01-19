import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { ApplicantService } from "../../services/applicant/applicant-service";
import Applicant from "../../models/applicant";

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
