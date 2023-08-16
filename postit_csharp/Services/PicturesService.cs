using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postit_csharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _picturesRepository;

  public PicturesService(PicturesRepository picturesRepository)
  {
    _picturesRepository = picturesRepository;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    int pictureId = _picturesRepository.CreatePicture(pictureData);

    Picture picture = GetPictureById(pictureId);

    return picture;
  }


  internal Picture GetPictureById(int pictureId)
  {
    Picture picture = _picturesRepository.GetPictureById(pictureId);

    if (picture == null)
    {
      throw new Exception("BAD ID");
    }

    return picture;
  }
}
