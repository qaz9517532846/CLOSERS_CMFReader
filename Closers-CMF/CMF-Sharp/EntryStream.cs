using System;
using System.ComponentModel;
using System.IO;

namespace Leayal.Closers.CMF
{
    internal class EntryStream : Stream
    {
        private bool _leaveOpen;
        internal Stream BaseStream { get; }
        public override bool CanRead => this.BaseStream.CanRead;
        public override bool CanSeek => this.BaseStream.CanSeek;
        public override bool CanWrite => false;
        public override bool CanTimeout => this.BaseStream.CanTimeout;
        public override int ReadTimeout { get => this.BaseStream.ReadTimeout; set => this.BaseStream.ReadTimeout = value; }
        public override int WriteTimeout { get => this.BaseStream.WriteTimeout; set => this.BaseStream.WriteTimeout = value; }

        private long _fixedLength;
        private long _offset;
        public override long Length => this._fixedLength;
        public override long Position
        {
            get => (this.BaseStream.Position - this._offset);
            set
            {
                if (value < 0 || value > this.Length)
                    throw new InvalidOperationException();
                this.BaseStream.Position = this._offset + value;
            }
        }

        public bool EndOfStream => !(this.Position < this.Length);

        internal EntryStream(Stream source, long offset, long entryPackedSize, bool leaveOpen)
        {
            this._leaveOpen = leaveOpen;
            this._offset = offset;
            this._fixedLength = entryPackedSize;
            this.BaseStream = source;
        }

        public override void Flush()
        {
            // Do nothing
            // this.BaseStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            long newpos, mypos;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    if (offset < 0 || offset > this.Length)
                        throw new InvalidOperationException();
                    newpos = this.BaseStream.Seek(this._offset + offset, origin);
                    break;
                case SeekOrigin.Current:
                    if (offset == 0)
                        return this.Position;
                    mypos = this.Position + offset;
                    if (mypos < this._offset)
                        newpos = this.BaseStream.Seek(this._offset, origin);
                    else if (mypos > (this._offset + this.Length))
                        throw new InvalidOperationException();
                    else
                        newpos = this.BaseStream.Seek(mypos, origin);
                    break;
                case SeekOrigin.End:
                    if (offset <= (-1 * this.Length) || offset > 0)
                        throw new InvalidOperationException();
                    newpos = this.BaseStream.Seek((this._offset + this.Length) + offset, origin);
                    break;
                default:
                    newpos = this.Position;
                    break;
            }
            return newpos;
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
            // this.BaseStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long howmuchleft = (this.Length - this.Position);
            if (howmuchleft < count)
                return this.BaseStream.Read(buffer, offset, (int)howmuchleft);
            else
                return this.BaseStream.Read(buffer, offset, count);
        }

        public override int ReadByte()
        {
            if (this.Position < this.Length)
                return this.BaseStream.ReadByte();
            else
                return -1;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
            // this.BaseStream.Write(buffer, offset, count);
        }

        public byte[] ReadToEnd()
        {
            byte[] result = new byte[this.Length];
            this.BaseStream.Read(result, 0, result.Length);
            return result;
        }

        public override void WriteByte(byte value)
        {
            throw new InvalidOperationException();
            // this.BaseStream.WriteByte(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this._leaveOpen)
                    this.BaseStream.Dispose();
            }
            base.Dispose(disposing);
        }

#pragma warning disable 0809
        [Obsolete("Should not use this.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            long howmuchleft = (this.Length - this.Position);
            if (howmuchleft < count)
                return this.BaseStream.BeginRead(buffer, offset, (int)howmuchleft, callback, state);
            else
                return this.BaseStream.BeginRead(buffer, offset, count, callback, state);
        }
        [Obsolete("Should not use this.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this.BaseStream.BeginWrite(buffer, offset, count, callback, state);
        }
        [Obsolete("Should not use this.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override int EndRead(IAsyncResult asyncResult)
        {
            return this.BaseStream.EndRead(asyncResult);
        }
        [Obsolete("Should not use this.", false), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public override void EndWrite(IAsyncResult asyncResult)
        {
            this.BaseStream.EndWrite(asyncResult);
        }
#pragma warning restore 0809
    }
}
