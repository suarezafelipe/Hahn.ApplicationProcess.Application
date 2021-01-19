import { autoinject } from "aurelia-framework";
import { HttpClient, json } from "aurelia-fetch-client";
import * as environment from "../../../config/environment.json";
import { Applicant } from "../../models/applicant";

@autoinject
export class ApplicantService {
  constructor(private httpClient: HttpClient) {
    httpClient.configure((config) => {
      config.useStandardConfiguration();
      config.withBaseUrl(environment.apiUrl);
    });
  }

  createApplicant(applicant: Applicant) {
    return this.httpClient
      .fetch("applicant", {
        method: "post",
        body: json(applicant),
      })
      .then((response) => response.json())
      .then((applicant) => applicant)
      .catch((error) => console.log(error));
  }

  getApplicant(id: number) {
    return this.httpClient
      .fetch(`applicant/${id}`)
      .then((response) => response.json())
      .then((applicant) => applicant);
  }
}
