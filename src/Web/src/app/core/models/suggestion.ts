//Name word getoont op de chip
//Explanation wordt getoont aan de bot zodat hij begrijpt wat de gebruiker wilt doen
export class Suggestion {
    name: string;
    explanation: string;

    constructor(name : string, explanation: string) {
      this.name = name;
      this.explanation = explanation;
    }
}
