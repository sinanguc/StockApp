export interface GenericResult<T> {

  result: boolean;
  message: string;
  errorMessage: string;
  errorCode: string;
  response: T;

}
