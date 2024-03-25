import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog',
  standalone: true,
  imports: [MatDialogTitle, MatDialogContent],
  templateUrl: './dialog.component.html',
  styleUrl: './dialog.component.scss'
})
export class DialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
  }

  convertImg(type: string, bytes: string[]){
    var base64Content = `data:${type};base64,${bytes}`;
    return base64Content;
  }
}


