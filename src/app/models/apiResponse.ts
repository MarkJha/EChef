export interface ApiResponse<T>{
    message:string,
    isError:boolean,
    errorMessage: string,
    pageSize: number,
    pageNumber: number,
    model:T[]
}