import { autoinject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
import * as environment from "../../../config/environment.json";
import { Applicant } from "../../models/applicant";
import axios from "axios";

@autoinject
export class ApplicantService {
  constructor(private httpClient: HttpClient) {
    httpClient.configure((config) => {
      config.useStandardConfiguration();
      config.withBaseUrl(environment.apiUrl);
    });
  }

  createApplicant(applicant: Applicant) {
    return axios
      .post(`${environment.apiUrl}applicant`, applicant)
      .then(({ data }) => data)
      .catch(({ response }) => {
        throw Error(
          response.status !== 400
            ? "There was a server or network error. Please try again later"
            : JSON.stringify(response.data.errors)
        );
      });
  }

  getApplicant(id: number) {
    return this.httpClient
      .fetch(`applicant/${id}`)
      .then((response) => response.json())
      .then((applicant) => applicant);
  }
}
