﻿using System;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Junior.Route.Routing.Responses.Application
{
	public class AtomResponse : ImmutableResponse
	{
		public AtomResponse(Func<byte[]> content, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().Content(content), configurationDelegate)
		{
		}

		public AtomResponse(Func<byte[]> content, Encoding encoding, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().ContentEncoding(encoding).Content(content), configurationDelegate)
		{
		}

		public AtomResponse(Func<string> content, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().Content(content), configurationDelegate)
		{
		}

		public AtomResponse(Func<string> content, Encoding encoding, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().ContentEncoding(encoding).Content(content), configurationDelegate)
		{
		}

		public AtomResponse(byte[] content, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().Content(content), configurationDelegate)
		{
		}

		public AtomResponse(byte[] content, Encoding encoding, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().ContentEncoding(encoding).Content(content), configurationDelegate)
		{
		}

		public AtomResponse(string content, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().Content(content), configurationDelegate)
		{
		}

		public AtomResponse(string content, Encoding encoding, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().ContentEncoding(encoding).Content(content), configurationDelegate)
		{
		}

		public AtomResponse(XmlNode content, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().Content(content.GetString()), configurationDelegate)
		{
		}

		public AtomResponse(XmlNode content, Encoding encoding, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().ContentEncoding(encoding).Content(content.GetBytes(encoding)), configurationDelegate)
		{
		}

		public AtomResponse(XNode content, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().Content(content.GetString()), configurationDelegate)
		{
		}

		public AtomResponse(XNode content, Encoding encoding, Action<Response> configurationDelegate = null)
			: base(new Response().ApplicationAtom().ContentEncoding(encoding).Content(content.GetBytes(encoding)), configurationDelegate)
		{
		}
	}
}