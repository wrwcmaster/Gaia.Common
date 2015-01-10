using Gaia.Common.Execute.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaia.Common.IO
{
    public static class StreamExtension
    {
        private const int DEFAULT_BUFFER_LENGTH = 1024 * 32;
        public static void CopyTo(this Stream inputStream, Stream outputStream, long size, IExecutionControl control)
        {
            CopyTo(inputStream, outputStream, size, DEFAULT_BUFFER_LENGTH, control);
        }

        public static void CopyTo(this Stream inputStream, Stream outputStream, long size, int bufferSize, IExecutionControl control)
        {
            if (inputStream == null || outputStream == null) return;
            try{
                byte[] buffer = new byte[bufferSize];
                int len = 0;
                int currentProgress = 0;
                long currentLength = 0;

                while ((len = inputStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    if(control != null && size > 0){
                        if(control.isCancelled()) break;
                        currentLength += len;
                        int p = (int)(currentLength * 100 / size);
                        if(p != currentProgress){
                            currentProgress = p;
                            control.PublishProgress(new ExecutionProgress(p));
                        }
                    }
                    outputStream.Write(buffer, 0, len);
                }
                if (control != null) control.PublishProgress(new ExecutionProgress(ExecutionProgress.MAX_PROGRESS));
            }finally{
                inputStream.Close();
                outputStream.Flush();
            }
        }
    }
}
