import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  baseUrl = 'http://localhost:5296/api/';
  resultMessage: string;
  constructor() {}
}

export class Result {
  MyResult: object;
  IsSuccess: boolean;
  ResultMessage: string;
  ResultCode: number;
}
