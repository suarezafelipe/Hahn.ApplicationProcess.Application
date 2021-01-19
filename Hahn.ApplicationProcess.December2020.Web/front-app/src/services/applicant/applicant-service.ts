import { autoinject } from "aurelia-framework";
import { I18N } from "aurelia-i18n";
import axios from "axios";
import Applicant from "../../models/applicant";
import * as environment from "../../../config/environment.json";

@autoinject
export class ApplicantService {
  constructor(private i18n: I18N) {}

  createApplicant(applicant: Applicant) {
    return axios
      .post(`${environment.apiUrl}applicant`, applicant, {
        headers: {
          locale: this.i18n.getLocale(),
        },
      })
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
    return axios
      .get(`${environment.apiUrl}applicant/${id}`)
      .then(({ data }) => data);
  }
}
