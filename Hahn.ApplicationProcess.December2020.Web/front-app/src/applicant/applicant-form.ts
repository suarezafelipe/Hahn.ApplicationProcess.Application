interface Applicant {
  name: string;
  familyName: string;
  address: string;
  countryOfOrigin: string;
  email: string;
  age: number;
  hired: boolean;
}

export class ApplicantForm {
  public message = "hello from applicant form";
  applicant: Applicant;  

  submit() {
    console.log(this.applicant);    
    // To prevent default browser's behaviour of reloading the page
    return false;
  }
}

