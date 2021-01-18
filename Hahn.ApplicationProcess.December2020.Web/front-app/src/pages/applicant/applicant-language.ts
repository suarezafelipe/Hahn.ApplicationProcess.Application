import { autoinject } from "aurelia-framework";
import { I18N } from "aurelia-i18n";

@autoinject
export class ApplicantLanguage {
  currentLocale: string;

  constructor(private i18n: I18N) {
    this.currentLocale = this.i18n.getLocale();
  }

  setLocale(newLocale: string) {
    this.i18n.setLocale(newLocale);
    this.currentLocale = newLocale;
  }
}
