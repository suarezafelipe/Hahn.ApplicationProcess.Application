import { autoinject } from "aurelia-framework";
import { I18N } from "aurelia-i18n";
import { EventAggregator } from "aurelia-event-aggregator";

@autoinject
export class ApplicantLanguage {
  currentLocale: string;

  constructor(private i18n: I18N, private ea: EventAggregator) {
    this.currentLocale = this.i18n.getLocale();
  }

  setLocale(newLocale: string) {
    this.i18n.setLocale(newLocale).then(() => {
      this.ea.publish("locale-changed", new Date());
    });
    this.currentLocale = newLocale;
  }
}
