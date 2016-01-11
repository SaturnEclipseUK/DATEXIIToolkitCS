using System.Text;

namespace DATEXIIToolkit.Services
{
    internal class UpdateMessage
    {
        private byte[] buffer;

        public UpdateMessage(byte[] xmlBuffer)
        {
            this.buffer = xmlBuffer;
        }

        public byte[] Buffer
        {
            get
            {
                return buffer;
            }

            set
            {
                buffer = value;
            }
        }
    }
}