using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

namespace CLOSERS_CMFReader.Classes
{
    class File
    {
        public File()
        {
            this.Size = null;
        }

        public File(Leayal.Closers.CMF.CMFEntry entry)
        {
            this.Name = entry.FileName;
            this.Size = entry.UnpackedSize;
        }

        private FileType filetype;
        private string _name;
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                this.filetype = FileType.DetermineByFilename(value);
            }
        }

        private long? _size;
        public long? Size
        {
            get => this._size;
            set
            {
                this._size = value;
                if (value.HasValue)
                    this.SizeInString = (new ByteSize(value.Value)).ToString();
                else
                    this.SizeInString = TheFamousUnknown;
            }
        }

        public string SizeInString { get; private set; }

        public string Type => this.filetype.Description;

        public BitmapImage Icon => this.filetype.Icon;

#region "FileType"
        public const string TheFamousUnknown = "Unknown";

        private class FileType
        {
            public string Description { get; }
            public BitmapImage Icon { get; }

            public FileType(string desc, BitmapImage image = null)
            {
                this.Description = desc;
                this.Icon = image;
            }

            public override string ToString()
            {
                return this.Description;
            }

            private static BitmapImage GetBitmapImage(string path)
            {
                BitmapImage result = new BitmapImage();

                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnDemand;
                result.CreateOptions = BitmapCreateOptions.DelayCreation;
                result.UriSource = new Uri(path, UriKind.Relative);
                result.DownloadCompleted += (sender, e) => { ((BitmapImage)sender).Freeze(); };
                result.EndInit();

                return result;
            }


            public static readonly FileType GraphicsObject = new FileType("Graphics Object", GetBitmapImage("/SharedResources;Component/Images/Icons/Graphics-Object.png"));
            public static readonly FileType Image = new FileType("Image/Texture", GetBitmapImage("/SharedResources;Component/Images/Icons/Image.png"));
            public static readonly FileType Script = new FileType("Script", GetBitmapImage("/SharedResources;Component/Images/Icons/Script.png"));
            public static readonly FileType Effect = new FileType("Effect", GetBitmapImage("/SharedResources;Component/Images/Icons/Script.png"));
            public static readonly FileType Sound = new FileType("Sound", GetBitmapImage("/SharedResources;Component/Images/Icons/Sound.png"));
            public static readonly FileType Unknown = new FileType(TheFamousUnknown, GetBitmapImage("/SharedResources;Component/Images/Icons/General.png"));

            private readonly static Dictionary<string, FileType> dict_exts = CreateDict();

            static Dictionary<string, FileType> CreateDict()
            {
                Dictionary<string, FileType> result = new Dictionary<string, FileType>(StringComparer.OrdinalIgnoreCase);

                // Graphics Object
                result.Add("x", GraphicsObject);

                // Image or Texture
                result.Add("dds", Image);
                result.Add("png", Image);
                result.Add("tga", Image);
                result.Add("bmp", Image);
                result.Add("psd", Image);

                // Script
                result.Add("lua", Script);
                result.Add("tet", Script);
                result.Add("xet", Script);

                // Effect (shader???)
                result.Add("fx", Effect);

                // Sound
                result.Add("ogg", Sound);
                result.Add("wav", Sound);

                // The rest
                result.Add(string.Empty, Unknown);
                return result;
            }

            public static FileType DetermineByFilename(string filename) => DetermineByExtension(Path.GetExtension(filename));

            public static FileType DetermineByExtension(string extension)
            {
                if (string.IsNullOrEmpty(extension))
                    return dict_exts[string.Empty];
                if (extension.StartsWith("."))
                    extension = extension.Remove(0, 1);
                if (dict_exts.ContainsKey(extension))
                    return dict_exts[extension];
                else
                    return dict_exts[string.Empty];
            }

            public static BitmapImage GetIconByFilename(string filename) => GetIconByExtension(Path.GetExtension(filename));

            public static BitmapImage GetIconByExtension(string extension) => DetermineByExtension(extension).Icon;
        }
#endregion
    }
}
