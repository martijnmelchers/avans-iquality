//text word getoont op de chip
//value wordt getoont aan de bot zodat hij begrijpt wat de gebruiker wilt doen
export class Suggestion {
    text: string;
    value: string;
    selected: boolean;

    constructor(name : string, explanation: string) {
      this.text = name;
      this.value = explanation;
    }
}
