export interface BaseResponse<TModel> {
    isSuccess: boolean;
    message: string;
    rows?: number;
    data?: TModel;
    userId?: number;
    description?: string;
  }