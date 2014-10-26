### 1.4 (3/3/2013)

**New Features**

  * [#2](https://github.com/dblock/resourcelib/pull/2): Added `VersionResource.FileFlags` - [@alaendle](https://github.com/alaendle).

**Misc**

  * Updated solution to Visual Studio 2010 - [@redwyre](https://github.com/redwyre).
  * Added NUGet references for `NUnit`, `NUnit.Runners`, and `MSBuildTasks` - [@redwyre](https://github.com/redwyre).
  * Added more options to build.cmd: `code`, `code_and_test` and `run_test_only` - [@redwyre](https://github.com/redwyre).
  * Documentation rewritten in markdown - [@dblock](https://github.com/dblock).

### 1.3 (5/28/2012

**Misc**

  * First release off [Github](https://github.com/dblock/resourcelib).
  * Added `LoadException` that is thrown on a resource enumeration failure that contains both the resource load (inner) exception and the outer Win32 resource enumeration (outer) exception.
  * Added `ResourceId.TypeName` that returns the string representation of the resource type when the ID represents one.
  * Added documentation on contributing and setting up a development environment.

**Bugs**

  * Bug: `ManifestResource.LoadFrom(string filename, ManifestType manifestType)` broken.
  * Bug: `ManifestResource.Manifest` fails to load Unicode XML manifests with BOM.
  * Bug: `DeviceIndependentBitmap.Image` cuts bitmap height in half.
  * Bug: `RT_MENU` broken for extended `MENUEX` resources.

### 1.2 (9/22/2009)

**New Features**

  * Added support for `RT_MANIFEST`, Windows SxS XML resources.
  * Added support for `RT_GROUP_CURSOR` and `RT_CURSOR` cursor resources, including loading .cur files and manipulating hotspot information.
  * Added support for `RT_BITMAP`, bitmap resources.
  * Added support for `RT_DIALOG`, dialog resources.
  * Added support for `RT_STRING`, string resources.
  * Added support for `RT_MENU`, menu resources.
  * Added support for `RT_ACCELERATOR`, accelerator resources.
  * Added limited support for `RT_FONTDIR` and `RT_FONT`, font resources.

**Misc**

  * Added support for version resources with an omitted `VS_FIXEDFILEINFO`.
  * Added `Resource.TypeName` that provides a string representation of the resource type.
  * Added `StringTable.CodePage` and `StringTable.LanguageID` properties.
  * Added `GenericResource.Data` read-only data bytes for unsupported resource types.
  * Interface change: renamed `StringResource` to `StringTableEntry` to accommodate for actual string resource support.
  * Interface change: added `ResourceId` that represents well-known and custom resources alike and provides comparison and hashing that works for all resource Id types. Both `Resource.Name` and `Resource.Type` now return `ResourceId` and public interfaces that accepted an `IntPtr` now require a `ResourceId`.
  * Interface change: `GroupIconResource` was renamed to `IconDirectoryResource`.
  * `Resource.Name` is no longer read-only.
  * `IconImage` was extended and renamed to `DeviceIndependentBitmap`. The latter supports separating mask and color, etc.
  * Automatically appending a second null-terminator to `StringResource` when required. Internal storage is now always with two null terminators.
  * Added VersionResource.ToString() that returns a standard resource file string representation of the version resource and all its tables.

**Bugs**

  * Bug: error deleting an English version resource which was loaded as language-neutral.
  * Bug: custom resources with literal string names return an invalid value in ` Resource.Name` and `Resource.Type`.
  * Bug: `StringResource` length in its header is incorrect after the value is updated.
  * Bug: `VersionResource.Write` erroneously included padding in the structure size.

### 1.1 (2/19/2009)

  * First release off [CodePlex](http://resourcelib.codeplex.com).
  * Added support for `RT_GROUP_ICON` and `RT_ICON`, icon resources.

### 1.0 (6/30/2008)

  * First release, [ CodeProject Article](http://www.codeproject.com/KB/library/ResourceLib.aspx).
  * Support for `RT_VERSION` and `VS_VERSIONINFO`, version resources.

