using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL.WPF
{
    /// <summary>
    /// Used to specify explictly a version of OpenGL.
    /// </summary>
    public enum OpenGLVersion
    {
        /// <summary>
        /// Version 1.1
        /// </summary>
        [Version(1, 1)]
        OpenGL1_1,

        /// <summary>
        /// Version 1.2
        /// </summary>
        [Version(1, 2)]
        OpenGL1_2,

        /// <summary>
        /// Version 1.3
        /// </summary>
        [Version(1, 3)]
        OpenGL1_3,

        /// <summary>
        /// Version 1.4
        /// </summary>
        [Version(1, 4)]
        OpenGL1_4,

        /// <summary>
        /// Version 1.5
        /// </summary>
        [Version(1, 5)]
        OpenGL1_5,

        /// <summary>
        /// OpenGL 2.0
        /// </summary>
        [Version(2, 0)]
        OpenGL2_0,

        /// <summary>
        /// OpenGL 2.1
        /// </summary>
        [Version(2, 1)]
        OpenGL2_1,

        /// <summary>
        /// OpenGL 3.0. This is the first version of OpenGL that requires a specially constructed render context.
        /// </summary>
        [Version(3, 0)]
        OpenGL3_0,

        /// <summary>
        /// OpenGL 3.1
        /// </summary>
        [Version(3, 1)]
        OpenGL3_1,

        /// <summary>
        /// OpenGL 3.2
        /// </summary>
        [Version(3, 2)]
        OpenGL3_2,

        /// <summary>
        /// OpenGL 3.3
        /// </summary>
        [Version(3, 3)]
        OpenGL3_3,

        /// <summary>
        /// OpenGL 4.0
        /// </summary>
        [Version(4, 0)]
        OpenGL4_0,

        /// <summary>
        /// OpenGL 4.1
        /// </summary>
        [Version(4, 1)]
        OpenGL4_1,

        /// <summary>
        /// OpenGL 4.2
        /// </summary>
        [Version(4, 2)]
        OpenGL4_2,

        /// <summary>
        /// OpenGL 4.3
        /// </summary>
        [Version(4, 3)]
        OpenGL4_3,

        /// <summary>
        /// OpenGL 4.4
        /// </summary>
        [Version(4, 4)]
        OpenGL4_4
    }


    /// <summary>
    /// Allows a version to be specified as metadata on a field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class VersionAttribute : Attribute
    {
        private readonly int major;
        private readonly int minor;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionAttribute"/> class.
        /// </summary>
        /// <param name="major">The major version number.</param>
        /// <param name="minor">The minor version number.</param>
        public VersionAttribute(int major, int minor)
        {
            this.major = major;
            this.minor = minor;
        }

        /// <summary>
        /// Determines whether this version is at least as high as the version specified in the parameters.
        /// </summary>
        /// <param name="major">The major version.</param>
        /// <param name="minor">The minor version.</param>
        /// <returns>True if this version object is at least as high as the version specified by <paramref name="major"/> and <paramref name="minor"/>.</returns>
        public bool IsAtLeastVersion(int major, int minor)
        {
            //  If major versions match, we care about minor. Otherwise, we only care about major.
            if (this.major == major)
                return this.major >= major && this.minor >= minor;
            return this.major >= major;
        }

        /// <summary>
        /// Gets the version attribute of an enumeration value <paramref name="enumeration"/>.
        /// </summary>
        /// <param name="enumeration">The enumeration.</param>
        /// <returns>The <see cref="VersionAttribute"/> defined on <paramref name="enumeration "/>, or null of none exists.</returns>
        public static VersionAttribute GetVersionAttribute(Enum enumeration)
        {
            //  Get the attribute from the enumeration value (if it exists).
            return enumeration
                    .GetType()
                    .GetMember(enumeration.ToString())
                    .Single()
                    .GetCustomAttributes(typeof(VersionAttribute), false)
                    .OfType<VersionAttribute>()
                    .FirstOrDefault();
        }

        /// <summary>
        /// Gets the major version number.
        /// </summary>
        public int Major
        {
            get { return major; }
        }

        /// <summary>
        /// Gets the minor version number.
        /// </summary>
        public int Minor
        {
            get { return minor; }
        }
    }
}
