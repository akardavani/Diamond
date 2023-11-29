namespace Diamond.Utils
{
    public static class Helper
    {
        public static T Retry<T>(Func<T> func, int retryCount, TimeSpan delayBetweenRetries)
        {
            int retries = 0;
            do
            {
                try
                {
                    // اجرای متد
                    return func();
                }
                catch (Exception ex)
                {
                    // اگر خطایی رخ داد، منتظر بمان تا زمان مشخصی
                    retries++;
                    if (retries <= retryCount)
                    {
                        Console.WriteLine($"Retry {retries} after error: {ex.Message}");
                        Thread.Sleep(delayBetweenRetries);
                    }
                    else
                    {
                        // اگر تعداد تکرارها تمام شد، خطا را به‌صورت اصلی پرتاب کن
                        throw;
                    }
                }
            } while (retries <= retryCount);

            // این بخش هرگز اجرا نمی‌شود، اما برای حفظ ساختار کد اضافه شده است
            return default;
        }

        public static async Task RetryAsync(Func<Task> asyncFunc, int retryCount, TimeSpan delayBetweenRetries)
        {
            int retries = 0;
            do
            {
                try
                {
                    // اجرای متد
                    await asyncFunc();
                    return;
                }
                catch (Exception ex)
                {
                    // اگر خطایی رخ داد، منتظر بمان تا زمان مشخصی
                    retries++;
                    if (retries <= retryCount)
                    {
                        Console.WriteLine($"Retry {retries} after error: {ex.Message}");
                        await Task.Delay(delayBetweenRetries);
                    }
                    else
                    {
                        // اگر تعداد تکرارها تمام شد، خطا را به‌صورت اصلی پرتاب کن
                        throw;
                    }
                }
            } while (retries <= retryCount);
        }
    }
}
